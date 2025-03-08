namespace VMWeb.Models
{
    public class WarehouseOutDetail
    {
        public int ProductId { get; set; }
        public int QuantityOut { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
