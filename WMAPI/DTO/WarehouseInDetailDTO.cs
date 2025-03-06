namespace WMAPI.DTO
{
    public class WarehouseInDetailDTO
    {
        public int ProductId { get; set; }
        public int QuantityIn { get; set; }
        public decimal PriceIn { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
