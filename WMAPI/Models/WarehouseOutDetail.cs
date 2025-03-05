using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class WarehouseOutDetail
    {
        public int OutDetailId { get; set; }
        public int OutId { get; set; }
        public int ProductId { get; set; }
        public int QuantityOut { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual WarehouseOut Out { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
