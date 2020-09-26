using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catelog.API.Entities;
using Catelog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catelog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatelogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatelogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

    }
}
