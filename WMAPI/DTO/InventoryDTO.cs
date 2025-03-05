namespace WMAPI.DTO
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
