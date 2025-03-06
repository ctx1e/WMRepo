namespace WMAPI.DTO
{
    public class WarehouseOutDetailDTO
    {
        public int ProductId { get; set; }
        public int QuantityOut { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
