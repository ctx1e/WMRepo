using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using VMWeb.Models;
using VMWeb.Models.ViewModel;
using VMWeb.Service;
using static System.Net.Mime.MediaTypeNames;

namespace VMWeb.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;

        }
        public async Task<IActionResult> ProductView(int page = 1, int pageSize = 8)
        {
            var productData = await _productService.GetProductsAsync();

            // Tính toán phân trang
            int totalItems = productData.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = productData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);
        }

        public async Task<IActionResult> ProductDetail(int proId)
        {
            var getProById = await _productService.GetProductByIdAsync(proId);
            if (getProById == null)
            {
                getProById = new Models.ViewModel.ProductViewModel();
                return View(getProById);
            }

            var viewModel = new ProductViewModel
            {
                ProductId = getProById.ProductId,
                ProductName = getProById.ProductName,
                Category = getProById.Category,
                Description = getProById.Description,
                Image = getProById.Image,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ProductAdd()
        {
            return View();
        }

        public async Task<IActionResult> HandleProduct(Product product)
        {

            // Kiểm tra kích thước file (ở đây là giới hạn 10MB)
            const long MaxFileSize = 10 * 1024 * 1024; // 10MB

            if (product.Image.Length > MaxFileSize)
            {
                ModelState.AddModelError("Image", "File size must be less than 10MB.");
                return RedirectToAction("product");
            }
            else
            {
                var data = await _productService.GetProductByIdAsync(product.ProductId);
                return RedirectToAction("a");
            }


        }
    }
}
