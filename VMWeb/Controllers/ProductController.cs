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
        public async Task<IActionResult> ProductView(int page = 1, int pageSize = 5)
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
            return View(new Product());
        }

        public async Task<IActionResult> HandleAddProduct(Product product)
        {


            // Kiểm tra kích thước file (ở đây là giới hạn 10MB)
            const long MaxFileSize = 10 * 1024 * 1024; // 10MB
            var checkIMG = product.Image.Length;
            if (product.Image.Length > MaxFileSize)
            {
                ModelState.AddModelError("Image", "File size must be less than 10MB.");
                return RedirectToAction("ProductAdd", product);
            }
            else
            {
                var check = await _productService.AddProductAsync(product);
                if (!check)
                {
                    TempData["ErrorMessage"] = "Failed to add product!";
                    return RedirectToAction("ProductAdd", product);
                }

                TempData["SuccessMessage"] = "Product added successfully";
                return RedirectToAction("ProductView");

            }

        }
            public async Task<IActionResult> HandleUpdateAndDeleteProduct(ProductViewModel productVM, string actionProduct)
        {
            if(actionProduct == "0")
            {
                // Kiểm tra kích thước file (ở đây là giới hạn 10MB)
                const long MaxFileSize = 10 * 1024 * 1024; // 10MB
                if (productVM.IFFImage != null && productVM.IFFImage.Length > 0 && productVM.IFFImage.Length > MaxFileSize)
                {
                    ModelState.AddModelError("Image", "File size must be less than 10MB.");
                    return RedirectToAction("product");
                }
                else
                {
                    var updateProduct = new Product
                    {
                        ProductId = productVM.ProductId,
                        ProductName = productVM.ProductName,
                        Category = productVM.Category,
                        Description = productVM.Description,
                        Image = productVM.IFFImage
                    };
                    var check = await _productService.UpdateProductAsync(updateProduct);
                    if (!check)
                    {
                        TempData["ErrorMessage"] = "Failed to add product!";
                        return RedirectToAction("ProductAdd");
                    }

                    TempData["SuccessMessage"] = "Updated Product successfully";
                    return RedirectToAction("ProductView");

                }

            } else
            {
                var check = await _productService.DeleteProductAsync(productVM.ProductId);

                if (!check)
                {
                    TempData["ErrorMessage"] = "Failed to Delete product!";
                    return RedirectToAction("ProductDetail");
                }

                TempData["SuccessMessage"] = "Delete product successfully";
                return RedirectToAction("ProductView");
            }


        }
    }
}
