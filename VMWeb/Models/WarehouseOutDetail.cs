namespace VMWeb.Models
{
    public class WarehouseOutDetail
    {
        public int OutDetailId { get; set; }
        public int OutId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityOut { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
