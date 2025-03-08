namespace VMWeb.Models
{
    public class WarehouseInDetail
    {
        public int ProductId { get; set; }
        public int QuantityIn { get; set; }
        public decimal PriceIn { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
