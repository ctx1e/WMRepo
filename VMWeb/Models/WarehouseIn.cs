namespace VMWeb.Models
{
    public class WarehouseIn
    {
        public int InId { get; set; }
        public string InCode { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
        public decimal TotalPriceIn { get; set; }
        public DateTime InDate { get; set; }
    }
}
