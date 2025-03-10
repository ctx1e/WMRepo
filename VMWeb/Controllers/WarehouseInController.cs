using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using VMWeb.Models;
using VMWeb.Models.ViewModel;
using VMWeb.Service;

namespace VMWeb.Controllers
{
    public class WarehouseInController : Controller
    {

        private readonly WarehouseInService _warehouseInService;

        public WarehouseInController(WarehouseInService warehouseInService)
        {
            _warehouseInService = warehouseInService;
        }
        public async Task<IActionResult> WarehouseInView(string searchTerm, int page = 1)
        {
            var warehouseList = await _warehouseInService.GetWarehouseInsAsync();  // API trả về List<WarehouseIn>

            // Áp dụng điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                warehouseList = warehouseList
                    .Where(w => w.InCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                w.SupplierName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Áp dụng phân trang
            int pageSize = 10;
            int totalRecords = warehouseList.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Lấy dữ liệu theo phân trang
            var pagedWarehouseList = warehouseList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;

            return View(pagedWarehouseList); // Trả về dữ liệu đã phân trang
        }

        public async Task<IActionResult> WarehouseInDetail(int inId)
        {
            var getWIByInId = await _warehouseInService.GetWarehouseInByInIdAsync(inId);
            var getAllWIByInId = await _warehouseInService.GetWIDByInIdAsync(inId);

            var getWIDVM = new WIDViewModel
            {
                WarehouseIn = getWIByInId,
                WarehouseInDetails = getAllWIByInId,
            };

            return View(getWIDVM);
        }


        public async Task<IActionResult> WarehouseInAdd()
        {
            return View(new WarehouseInAddResponse());
        }

        public async Task<IActionResult> HandleWIAdd(WarehouseInAddResponse model)
        {

            decimal totalPrice = 0;

            foreach (var item in model.WarehouseInDetailDTOs)
            {
                // Tính toán tổng tiền của từng sản phẩm (PriceIn * QuantityIn)
                item.TotalPrice = item.PriceIn * item.QuantityIn;

                // Cộng vào tổng tiền nhập kho
                totalPrice += item.TotalPrice;
            }

            // Cập nhật tổng tiền nhập vào model
            model.TotalPriceIn = totalPrice;
            try
            {
                // Gửi dữ liệu sang API để tạo warehouse in
                bool isSuccess = await _warehouseInService.AddWarehouseInAsync(model);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Warehouse In added successfully!";
                    return RedirectToAction("WarehouseInView");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add Warehouse In. Please try again.";
                    return RedirectToAction("WarehouseInAdd", model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return RedirectToAction("WarehouseInAdd",model);
            }

        }

        public async Task<IActionResult> HandleDelWI(int inId)
        {
            var getWI = await _warehouseInService.GetWarehouseInByInIdAsync(inId);
            bool isSuccess = await _warehouseInService.DeleteWarehouseInAsync(inId);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = $"Delete WarehouseIn {getWI.InCode} successfully!";
                return RedirectToAction("WarehouseInView");
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to delete WarehouseIn {getWI.InCode}!. Please try again";
                return RedirectToAction("WarehouseInView");
            }
        }

    }
}
