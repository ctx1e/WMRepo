namespace VMWeb.Models
{
    public class WarehouseOut
    {
        public int OutId { get; set; }
        public string OutCode { get; set; } = null!;
        public string RecipientName { get; set; } = null!;
        public decimal TotalPriceOut { get; set; }
        public DateTime OutDate { get; set; }
    }
}
