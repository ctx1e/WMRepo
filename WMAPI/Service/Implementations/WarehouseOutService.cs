using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class WarehouseOutService : IWarehouseOutService
    {

        private readonly IWarehouseOutRepository _warehouseOutRepository;
        private readonly IWODRepository _wodRepository;
        private readonly IInventoryRepository _inventoryInRepository;

        public WarehouseOutService(IWarehouseOutRepository warehouseOutRepository, IInventoryRepository inventoryRepository, IWODRepository wodRepository)
        {
            _warehouseOutRepository = warehouseOutRepository;
            _wodRepository = wodRepository;
            _inventoryInRepository = inventoryRepository;
        }

        public async Task<(bool IsSuccess, string Msg)> AddWO(GetWarehouseOutRequest warehouseOutRequest)
        {
            foreach(var item in warehouseOutRequest.WarehouseOutDetailDTOs)
            {
                var checkQuanProductInInven = await _inventoryInRepository.GetProductInInventoryByProductId(item.ProductId);
                if((checkQuanProductInInven.QuantityInStock - item.QuantityOut) < 0)
                {
                    return (false, "The number of products shipped is greater than the number of products in stock." +
                                    " The number of products in stock is: " + checkQuanProductInInven.QuantityInStock);
                }
            }
            List<WarehouseOutDetail> getListWOD = new();
            List<Inventory> getListInventoryByProductId = new();


            DateTime dateNow = DateTime.Now;

            var getWO = new WarehouseOut
            {
                OutCode = warehouseOutRequest.RecipientName + "_" + "OUT" + await GenerateShortUniqueCode(),
                RecipientName = warehouseOutRequest.RecipientName,
                TotalPriceOut = warehouseOutRequest.TotalPriceOut,
                OutDate = dateNow
            };
            if (!(await _warehouseOutRepository.AddWO(getWO)))
                return (false, "Add Warehouse Out failed!");

            // Get List Inventory to update product and list Warehouse in detail to add
            foreach (var item in warehouseOutRequest.WarehouseOutDetailDTOs)
            {
                var getInvenByProId = await _inventoryInRepository.GetProductInInventoryByProductId(item.ProductId);
                if (getInvenByProId != null)
                {
                    getInvenByProId.QuantityInStock -= item.QuantityOut;
                    getInvenByProId.LastUpdated = dateNow;
                    getListInventoryByProductId.Add(getInvenByProId);
                }

                var getWOD = new WarehouseOutDetail
                {
                    OutId = getWO.OutId,
                    ProductId = item.ProductId,
                    QuantityOut = item.QuantityOut,
                    PriceOut = item.PriceOut,
                    TotalPrice = item.QuantityOut * item.PriceOut,
                };
                getListWOD.Add(getWOD);
            }

            if (!(await _wodRepository.AddMultiWODByInId(getListWOD)))
                return (false, "Add WOD by warehouse in failed!");

            if (!(await _inventoryInRepository.UpdateMultiInventory(getListInventoryByProductId)))
                return (false, "Update Inventories By Product Id failed!");

            return (true, "Add Warehouse Out successfully");
        }

        public async Task<(IEnumerable<WarehouseOut> GetWOs, string Msg)> GetAllWOs()
        {
            var getWOs = await _warehouseOutRepository.GetAllWO();

            if (!getWOs.Any())
            {
                return (Enumerable.Empty<WarehouseOut>(), "WarehouseIns is empty");
            }
            return (getWOs, "get WarehouseIns successfully");
        }

        public async Task<(WarehouseOut? GetWOs, string Msg)> GetWOById(int outId)
        {
            var getWOById = await _warehouseOutRepository.GetWOById(outId);
            if (getWOById == null)
            {
                return (null, "Not found WI By Id!");
            }
            return (getWOById, "Get WI By Id Successfully");
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
