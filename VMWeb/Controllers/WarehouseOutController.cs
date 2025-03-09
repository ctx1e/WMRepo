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
            int pageSize = 10;
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
    }
}
