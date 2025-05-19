using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Dtos.Shopping;
using Taller1.Src.Helpers;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Controllers
{
    public class ShoppingCartController(ILogger<ProductController> logger, UnitOfWork unitOfWork) : BaseController
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        [HttpGet]
        //ShoppingCartDto crear y usar metodo get
        public async Task<ActionResult<ApiResponse<ShoppingCartDto>>> GetShoppingCart()
        {
            var shoppingCart = await RetrieveShoppingCart();
            if (shoppingCart == null)
                return NoContent();

            return Ok(new ApiResponse<ShoppingCartDto>(
                true,
                "Carrito obtenido correctamente",
                shoppingCart.ToDto()
            ));
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ShoppingCartDto>>> AddItemToShoppingCart(int productId, int quantity)
        {
            _logger.LogWarning("Entrando a AddItemToBasket con productId: {ProductId}, quantity: {Quantity}", productId, quantity);

            var shoppingCart = await RetrieveShoppingCart();

            if (shoppingCart == null)
            {
                shoppingCart = CreateShoppingCart();
                await _unitOfWork.SaveChangeAsync();
            }

            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            if (product == null)
                return BadRequest(new ApiResponse<string>(false, "Producto no encontrado"));

            if (!product.IsActive)
                return BadRequest(new ApiResponse<string>(false, $"El producto '{product.Name}' está inactivo y no se puede agregar al carrito."));

            if (product.Stock == 0)
                return BadRequest(new ApiResponse<string>(false, $"El producto '{product.Name}' no tiene stock disponible."));

            if (product.Stock < quantity)
                return BadRequest(new ApiResponse<string>(false, $"Solo hay {product.Stock} unidades disponibles de '{product.Name}'"));

            shoppingCart.AddItem(product, quantity);

            var changes = await _unitOfWork.SaveChangeAsync();
            var success = changes > 0;

            return success
                ? CreatedAtAction(nameof(GetShoppingCart), new ApiResponse<ShoppingCartDto>(true, "Producto añadido al carrito", shoppingCart.ToDto()))
                : BadRequest(new ApiResponse<string>(false, "Ocurrió un problema al actualizar el carrito"));
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<ShoppingCartDto>>> RemoveItemFromShoppingCart(int productId, int quantity)
        {
            var shoppingCart = await RetrieveShoppingCart();
            if (shoppingCart == null)
                return BadRequest(new ApiResponse<string>(false, "Carrito no encontrado"));

            shoppingCart.RemoveItem(productId, quantity);

            var success = await _unitOfWork.SaveChangeAsync() > 0;

            return success
                ? Ok(new ApiResponse<ShoppingCartDto>(
                    true,
                    "Producto eliminado del carrito",
                    shoppingCart.ToDto()
                ))
                : BadRequest(new ApiResponse<string>(false, "Error al actualizar el carrito"));
        }

        private async Task<ShoppingCart?> RetrieveShoppingCart()
        {
            var cartId = Request.Cookies["cartId"];
            _logger.LogWarning("CartId recibido desde cookie: {CartId}", cartId);

            return string.IsNullOrEmpty(cartId)
                ? null
                : await _unitOfWork.ShoppingCartRepository.GetShoppingCartAsync(cartId);
        }

        private ShoppingCart CreateShoppingCart()
        {
            var cartId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(30),
            };

            Response.Cookies.Append("cartId", cartId, cookieOptions);
            _logger.LogWarning("Nuevo ShoppingCart creado con ID: {CartId}", cartId);

            return _unitOfWork.ShoppingCartRepository.CreateShoppingCart(cartId);
        }
    }
}