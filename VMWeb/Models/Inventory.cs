namespace VMWeb.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
