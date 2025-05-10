using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Interfaces;

[ApiController]
[Route("api/[controller]")]
namespace Taller1.Src.Controllers
{
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