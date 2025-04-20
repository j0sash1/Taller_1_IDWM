using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller1.Src.Data;
using Taller1.Src.Models;

namespace Taller1.Src.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly StoreContext _context;

        public ProductController(ILogger<ProductController> logger, StoreContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product == null ? (ActionResult<Product>)NotFound() : (ActionResult<Product>)Ok(product);
        }
    }
}