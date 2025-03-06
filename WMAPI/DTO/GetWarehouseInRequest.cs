namespace WMAPI.DTO
{
    public class GetWarehouseInRequest
    {
        public string SupplierName { get; set; } = null!;
        public decimal TotalPriceIn { get; set; }
        public List<WarehouseInDetailDTO> WarehouseInDetailDTOs { get; set; } = new List<WarehouseInDetailDTO>();
    }
}
