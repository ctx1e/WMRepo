using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WMAPI.DTO;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getListProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var (products, message) = await _productService.GetAllProducts();
            if (products == null && !(products.Any()))
            {
                return Ok(new { Message = message, Data = products });
            }
            return Ok(new { Message = message, Data = products });
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (product == null)
            {
                return BadRequest("product is null!!");
            }
            var (isSuccess, message) = await _productService.AddProduct(product);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (product == null)
            {
                return BadRequest("product is null!!");
            }

            var (isSuccess, message) = await _productService.UpdateProduct(product);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }

        [HttpDelete("deleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var (isSuccess, message) = await _productService.DeleteProduct(productId);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }
    }
}
