using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Taller1.Src.RequestHelpers;
using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Dtos.ShippingAddress;
using Taller1.Src.Dtos.User;
using Taller1.Src.Extensions;
using Taller1.Src.Helpers;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Controllers
{
    public class UserController(ILogger<UserController> logger, UnitOfWork unitOfWork) : BaseController
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly UnitOfWork _unitOfWork = unitOfWork;



        // GET /user?params...
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAll([FromQuery] UserParams userParams)
        {
            var query = _unitOfWork.UserRepository.GetUsersQueryable()
                .Filter(userParams.IsActive, userParams.RegisteredFrom, userParams.RegisteredTo)
                .Search(userParams.SearchTerm)
                .Sort(userParams.OrderBy);

            var total = await query.CountAsync();

            var users = await query
                .Skip((userParams.PageNumber - 1) * userParams.PageSize)
                .Take(userParams.PageSize)
                .ToListAsync();

            var dtos = users.Select(UserMapper.UserToUserDto).ToList();

            Response.AddPaginationHeader(new PaginationMetaData
            {
                CurrentPage = userParams.PageNumber,
                TotalPages = (int)Math.Ceiling(total / (double)userParams.PageSize),
                PageSize = userParams.PageSize,
                TotalCount = total
            });

            return Ok(new ApiResponse<IEnumerable<UserDto>>(true, "Usuarios obtenidos correctamente", dtos));
        }
        [Authorize(Roles = "Admin")]
        // GET /users/{id}
        [HttpGet("{email}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetById(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));

            var dto = UserMapper.UserToUserDto(user);
            return Ok(new ApiResponse<UserDto>(true, "Usuario encontrado", dto));
        }

        [Authorize(Roles = "Admin")]
        // PUT /users/{id}/status
        [HttpPatch("{email}/status")]
        public async Task<ActionResult<ApiResponse<string>>> ToggleStatus(string email, [FromBody] ToggleStatusDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));

            var roles = await _unitOfWork.UserRepository.GetUserRolesAsync(user);
            if (roles.Contains("Admin", StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(new ApiResponse<string>(
                    false,
                    "No se puede deshabilitar una cuenta con rol de administrador."
                ));
            }

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.SaveChangeAsync();

            var message = user.IsActive ? "Usuario habilitado correctamente" : "Usuario deshabilitado correctamente";
            return Ok(new ApiResponse<string>(true, message));
        }
        [Authorize(Roles = "User")]
        [HttpPost("address")]
        public async Task<ActionResult<ApiResponse<ShippingAddres>>> CreateShippingAddress([FromBody] CreateShippingAddressDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponse<string>(false, "Usuario no autenticado"));

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));
            var existing = await _unitOfWork.ShippingAddressRepository.GetByUserIdAsync(userId);
            var hasExistingData = existing != null && !string.IsNullOrWhiteSpace(existing.Street) &&
                !string.IsNullOrWhiteSpace(existing.Number) &&
                !string.IsNullOrWhiteSpace(existing.Commune) &&
                !string.IsNullOrWhiteSpace(existing.Region) &&
                !string.IsNullOrWhiteSpace(existing.PostalCode);

            if (hasExistingData)
                return BadRequest(new ApiResponse<string>(false, "Ya tienes una dirección registrada válida"));

            var address = ShippingAddressMapper.FromDto(dto, userId);

            await _unitOfWork.ShippingAddressRepository.AddAsync(address);
            await _unitOfWork.SaveChangeAsync();
            var addressDto = ShippingAddressMapper.ToDto(address);
            return Ok(new ApiResponse<ShippingAddressDto>(true, "Dirección creada exitosamente", addressDto));
        }


        [Authorize(Roles = "User")]
        [HttpPatch("profile")]
        public async Task<ActionResult<ApiResponse<UserDto>>> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiResponse<string>(false, "Usuario no autenticado"));

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));

            UserMapper.UpdateUserFromDto(user, dto);

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.SaveChangeAsync();

            return Ok(new ApiResponse<UserDto>(true, "Perfil actualizado correctamente", UserMapper.UserToUserDto(user)));
        }

        [Authorize(Roles = "User")]
        [HttpPatch("profile/password")]
        public async Task<ActionResult<ApiResponse<string>>> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiResponse<string>(false, "Usuario no autenticado"));

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest(new ApiResponse<string>(false, "La nueva contraseña y la confirmación no coinciden"));
            if (dto.NewPassword == dto.CurrentPassword) return BadRequest(new ApiResponse<string>(false, "La nueva contraseña no puede ser igual a la actual"));

            var result = await _unitOfWork.UserRepository.UpdatePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<string>(
                    false,
                    "Error al cambiar la contraseña",
                    null,
                    result.Errors.Select(e => e.Description).ToList()
                ));
            }

            return Ok(new ApiResponse<string>(true, "Contraseña actualizada correctamente"));
        }
        [Authorize(Roles = "User")]
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiResponse<string>(false, "Usuario no autenticado"));

            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new ApiResponse<string>(false, "Usuario no encontrado"));

            var dto = UserMapper.UserToUserDto(user);
            return Ok(new ApiResponse<UserDto>(true, "Perfil del usuario obtenido", dto));
        }
        [Authorize(Roles = "User")]
        [HttpPut("address")]
        public async Task<ActionResult<ApiResponse<ShippingAddres>>> UpdateShippingAddress([FromBody] CreateShippingAddressDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new ApiResponse<string>(false, "Usuario no autenticado"));

            var address = await _unitOfWork.ShippingAddressRepository.GetByUserIdAsync(userId);
            if (address == null)
                return NotFound(new ApiResponse<string>(false, "No tienes una dirección registrada. Usa el método POST para crear una."));


            address.Street = dto.Street;
            address.Number = dto.Number;
            address.Commune = dto.Commune;
            address.Region = dto.Region;
            address.PostalCode = dto.PostalCode;

            await _unitOfWork.SaveChangeAsync();

            return Ok(new ApiResponse<ShippingAddres>(true, "Dirección actualizada correctamente", address));
        }
    }
}