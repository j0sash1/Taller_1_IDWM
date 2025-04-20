using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller1.Src.Models;

using Taller1.Src.Data;

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
    }
}