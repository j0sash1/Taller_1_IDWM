using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Extensions;
using Taller1.Src.Helpers;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;
using Taller1.Src.RequestHelpers;


namespace Taller1.Src.Controllers
{
    public class ProductController(ILogger<ProductController> logger, IProductService productService) : BaseController
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> GetPaged([FromQuery] ProductParams productParams)
        {
            var pagedList = await _productService.GetPagedProductsAsync(productParams);

            if (pagedList == null || pagedList.Count == 0)
                return Ok(new ApiResponse<IEnumerable<Product>>(false, "No hay productos disponibles"));

            Response.AddPaginationHeader(pagedList.Metadata);

            return Ok(new ApiResponse<IEnumerable<Product>>(true, "Productos obtenidos correctamente", pagedList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Product>>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null
                ? NotFound(new ApiResponse<Product>(false, "Producto no encontrado"))
                : Ok(new ApiResponse<Product>(true, "Producto obtenido correctamente", product));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<Product>>> Create([FromForm] ProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);
            if (product == null)
                return BadRequest(new ApiResponse<Product>(false, "Error al crear el producto"));

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, new ApiResponse<Product>(true, "Producto agregado correctamente", product));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Product>>> Update(int id, [FromForm] ProductDto dto)
        {
            var product = await _productService.UpdateAsync(id, dto);
            if (product == null)
                return NotFound(new ApiResponse<Product>(false, "Producto no encontrado o error en actualización"));

            return Ok(new ApiResponse<Product>(true, "Producto actualizado correctamente", product));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (result == null)
                return NotFound(new ApiResponse<string>(false, "Producto no encontrado"));

            var message = result == "desactivado"
                ? "Producto desactivado correctamente (asociado a pedidos)"
                : "Producto eliminado correctamente";

            return Ok(new ApiResponse<string>(true, message));
        }
    }
}