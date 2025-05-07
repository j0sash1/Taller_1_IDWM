using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller1.Src.Data;
using Taller1.Src.Dtos;

using Taller1.Src.Models;

namespace Taller1.Src.Controllers
{
    public class UserController(ILogger<UserController> logger, UnitOfWork unitOfWork) : BaseController
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly UnitOfWork _unitOfWorkork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            // var users = await _unitOfWorkork.UserRepository.GetAllUsersAsync();
            var users = await _unitOfWorkork.UserManager.Users
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            if (userDto.ConfirmPassword != userDto.Password)
            {
                return BadRequest("Passwords do not match");
            }
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password,
                Thelephone = userDto.Thelephone,
                ShippingAddres = new ShippingAddres
                {
                    Street = userDto.Street ?? string.Empty,
                    Number = userDto.Number ?? string.Empty,
                    Commune = userDto.Commune ?? string.Empty,
                    Region = userDto.Region ?? string.Empty,
                    PostalCode = userDto.PostalCode ?? string.Empty
                }
            };
            await _unitOfWorkork.UserRepository.CreatedUserAsync(user, user.ShippingAddres);
            await _unitOfWorkork.SaveChangeAsync();
            return Ok(user);
        }
    }
}