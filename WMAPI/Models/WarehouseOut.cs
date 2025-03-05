using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class WarehouseOut
    {
        public WarehouseOut()
        {
            WarehouseOutDetails = new HashSet<WarehouseOutDetail>();
        }

        public int OutId { get; set; }
        public string OutCode { get; set; } = null!;
        public string RecipientName { get; set; } = null!;
        public decimal TotalPriceOut { get; set; }
        public DateTime OutDate { get; set; }

        public virtual ICollection<WarehouseOutDetail> WarehouseOutDetails { get; set; }
    }
}
