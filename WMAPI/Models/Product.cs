using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            WarehouseInDetails = new HashSet<WarehouseInDetail>();
            WarehouseOutDetails = new HashSet<WarehouseOutDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual Inventory? Inventory { get; set; }
        public virtual ICollection<WarehouseInDetail> WarehouseInDetails { get; set; }
        public virtual ICollection<WarehouseOutDetail> WarehouseOutDetails { get; set; }
    }
}
