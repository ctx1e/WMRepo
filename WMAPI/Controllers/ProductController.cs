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
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("getProductById/{proId}")]
        public async Task<IActionResult> GetWIById(int proId)
        {

            var proById= await _productService.GetProductById(proId);
            if (proById == null)
            {
                return Ok(proById);
            }
            return Ok(proById);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO product)
        {

            if (product == null)
            {
                return BadRequest("product is null!!");
            }
            var isSuccess = await _productService.AddProduct(product);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (product == null)
            {
                return BadRequest("product is null!!");
            }

            var isSuccess = await _productService.UpdateProduct(product);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }

        [HttpDelete("deleteProduct/{proId}")]
        public async Task<IActionResult> DeleteProduct(int proId)
        {
            var isSuccess = await _productService.DeleteProduct(proId);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }
    }
}
