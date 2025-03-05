using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
