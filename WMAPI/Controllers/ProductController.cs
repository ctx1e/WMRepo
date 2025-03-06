using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WMAPI.DTO;
using WMAPI.Service.Implementations;
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

        [HttpGet("getProductById/{proId}")]
        public async Task<IActionResult> GetWIById(int proId)
        {

            var (proById, message) = await _productService.GetProductById(proId);
            if (proById == null)
            {
                return Ok(new { Message = message, Data = proById });
            }
            return Ok(new { Message = message, Data = proById });
        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {

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

        [HttpDelete("deleteProduct/{proId}")]
        public async Task<IActionResult> DeleteProduct(int proId)
        {
            var (isSuccess, message) = await _productService.DeleteProduct(proId);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }
    }
}
