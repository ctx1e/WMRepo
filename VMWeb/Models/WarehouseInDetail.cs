namespace VMWeb.Models
{
    public class WarehouseInDetail
    {
        public int InDetailId { get; set; }
        public int InId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int QuantityIn { get; set; }
        public decimal PriceIn { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
