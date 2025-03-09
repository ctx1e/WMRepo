using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    }
}
