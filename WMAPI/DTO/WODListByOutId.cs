namespace WMAPI.DTO
{
    public class WODListByOutId
    {
        public int OutDetailId { get; set; }
        public int OutId { get; set; }
        public string ProductName { get; set; }
        public int QuantityOut { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
