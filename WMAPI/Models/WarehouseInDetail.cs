using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class WarehouseInDetail
    {
        public int InDetailId { get; set; }
        public int InId { get; set; }
        public int ProductId { get; set; }
        public int QuantityIn { get; set; }
        public decimal PriceIn { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual WarehouseIn In { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
