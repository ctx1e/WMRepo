using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Implementations;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class WarehouseInService : IWarehouseInService
    {

        private readonly IWarehouseInRepository _warehouseInRepository;
        private readonly IWIDRepository _widRepository;
        private readonly IInventoryRepository _inventoryInRepository;

        public WarehouseInService(IWarehouseInRepository warehouseInRepository, IInventoryRepository inventoryRepository, IWIDRepository widRepository)
        {
            _warehouseInRepository = warehouseInRepository;
            _inventoryInRepository = inventoryRepository;
            _widRepository = widRepository;
        }
        public async Task<bool> AddWI(GetWarehouseInRequest warehouseInRequest)
        {
            List<WarehouseInDetail> getListWID = new();
            List<Inventory> getListInventoryByProductId = new();

            DateTime dateNow = DateTime.Now;

            var getWI = new WarehouseIn
            {
                InCode = warehouseInRequest.SupplierName + "_" + "In_" + await GenerateShortUniqueCode(),
                SupplierName = warehouseInRequest.SupplierName,
                TotalPriceIn = warehouseInRequest.TotalPriceIn,
                InDate = dateNow
            };
            if (!(await _warehouseInRepository.AddWI(getWI)))
                return false;

            // Get List Inventory to update product and list Warehouse in detail to add
            foreach (var item in warehouseInRequest.WarehouseInDetailDTOs)
            {
                var getInvenByProId = await _inventoryInRepository.GetProductInInventoryByProductId(item.ProductId);
                if (getInvenByProId != null)
                {
                    getInvenByProId.QuantityInStock += item.QuantityIn;
                    getInvenByProId.LastUpdated = dateNow;
                    getListInventoryByProductId.Add(getInvenByProId);
                }

                var getWID = new WarehouseInDetail
                {
                    InId = getWI.InId,
                    ProductId = item.ProductId,
                    QuantityIn = item.QuantityIn,
                    PriceIn = item.PriceIn,
                    TotalPrice = item.QuantityIn * item.PriceIn,
                };
                getListWID.Add(getWID);
            }

            if (!(await _widRepository.AddMultiWIDByInId(getListWID)))
                return false;

            if (!(await _inventoryInRepository.UpdateMultiInventory(getListInventoryByProductId)))
                return false;

            return true;
        }

        public async Task<IEnumerable<WarehouseIn>> GetAllWIs()
        {
            var getWIs = await _warehouseInRepository.GetAllWI();

            if (!getWIs.Any())
            {
                return Enumerable.Empty<WarehouseIn>();
            }
            return getWIs;
        }

        public async Task<WarehouseIn?> GetWIById(int inId)
        {
            var getWIById = await _warehouseInRepository.GetWIById(inId);
            if (getWIById == null)
            {
                return null;
            }
            return getWIById;
        }

        public async Task<bool> DeleteWI(int inId)
        {
            //var getListWIDInId = await _widRepository.GetAllWIDByInId(inId);
            var getWIByInId = await GetWIById(inId);

            //if (getListWIDInId == null || !(getListWIDInId.Any())) return false;
            if (getWIByInId == null) return false;


            //if(!(await _widRepository.RemoveMultiWIDByWI(getListWIDInId))) 
            //    return false;

            if (!(await _warehouseInRepository.DeleteWI(getWIByInId)))
                return false;

            return true;
        }

        public async Task<string> GenerateShortUniqueCode()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            byte[] shortBytes = new byte[6];
            Array.Copy(bytes, 0, shortBytes, 0, 6); // Get 6 byte (48-bit)
            return Convert.ToBase64String(shortBytes).Substring(0, 8); // 8 characters
        }


    }
}
