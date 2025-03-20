using Microsoft.AspNetCore.Mvc;
using VMWeb.Models;
using VMWeb.Models.ViewModel;
using VMWeb.Service;

namespace VMWeb.Controllers
{
    public class WarehouseOutController : Controller
    {
        private readonly WarehouseOutService _warehouseOutService;

        public WarehouseOutController(WarehouseOutService warehouseOutService)
        {
            _warehouseOutService = warehouseOutService;
        }

        // Warehouse Out List
        public async Task<IActionResult> WarehouseOutView(string searchTerm, int page = 1)
        {
            var warehouseOutList = await _warehouseOutService.GetWarehouseOutsAsync();  // API trả về List<WarehouseOut>

            // Áp dụng điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                warehouseOutList = warehouseOutList
                    .Where(w => w.OutCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                w.RecipientName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Áp dụng phân trang
            int pageSize = 5;
            int totalRecords = warehouseOutList.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Lấy dữ liệu theo phân trang
            var pagedWarehouseOutList = warehouseOutList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;

            return View(pagedWarehouseOutList); // Trả về dữ liệu đã phân trang
        }

        // Warehouse Out Detail
        public async Task<IActionResult> WarehouseOutDetail(int outId)
        {
            var getWOByOutId = await _warehouseOutService.GetWarehouseOutByOutIdAsync(outId);
            var getAllWOByOutId = await _warehouseOutService.GetWODByOutIdAsync(outId);

            var getWODVM = new WODViewModel
            {
                WarehouseOut = getWOByOutId,
                WarehouseOutDetails = getAllWOByOutId,
            };

            return View(getWODVM);
        }

        public async Task<IActionResult> WarehouseOutAdd()
        {
            return View(new WarehouseOutAddResponse());
        }

        public async Task<IActionResult> HandleWOAdd(WarehouseOutAddResponse model)
        {

            decimal totalPrice = 0;

            foreach (var item in model.WarehouseOutDetailDTOs)
            {
                // Tính toán tổng tiền của từng sản phẩm (PriceIn * QuantityIn)
                item.TotalPrice = item.PriceOut * item.QuantityOut;

                // Cộng vào tổng tiền nhập kho
                totalPrice += item.TotalPrice;
            }

            // Cập nhật tổng tiền nhập vào model
            model.TotalPriceOut = totalPrice;
            try
            {
                // Gửi dữ liệu sang API để tạo warehouse in
                bool isSuccess = await _warehouseOutService.AddWarehouseOutAsync(model);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Warehouse Out added successfully!";
                    return RedirectToAction("WarehouseOutView");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add Warehouse Out. Please try again.";
                    return RedirectToAction("WarehouseOutAdd", model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return RedirectToAction("WarehouseOutAdd", model);
            }

        }

        public async Task<IActionResult> HandleDelWO(int outId)
        {
            var getWO = await _warehouseOutService.GetWarehouseOutByOutIdAsync(outId);
            bool isSuccess = await _warehouseOutService.DeleteWarehouseOutAsync(outId);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = $"Delete WarehouseOut {getWO.OutCode} successfully!";
                return RedirectToAction("WarehouseOutView");
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to delete WarehouseOut {getWO.OutCode}!. Please try again";
                return RedirectToAction("WarehouseOutView");
            }
        }

    }
}
