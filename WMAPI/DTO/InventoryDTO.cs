﻿namespace WMAPI.DTO
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
