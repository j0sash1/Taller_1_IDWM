using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Taller1.Src.Interfaces;
using Taller1.Src.Dtos;
using Taller1.Src.Data;
using Taller1.Src.Interfaces;

namespace Taller1.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;
        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId);
            return Ok(cart);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] AddItemDto dto)
        {
            await _cartService.AddItemAsync(dto.CartId, dto.ProductId, dto.Quantity);
            return NoContent();
        }
    }
}