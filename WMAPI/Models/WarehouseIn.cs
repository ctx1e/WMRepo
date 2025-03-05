using System;
using System.Collections.Generic;

namespace WMAPI.Models
{
    public partial class WarehouseIn
    {
        public WarehouseIn()
        {
            WarehouseInDetails = new HashSet<WarehouseInDetail>();
        }

        public int InId { get; set; }
        public string InCode { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
        public decimal TotalPriceIn { get; set; }
        public DateTime InDate { get; set; }

        public virtual ICollection<WarehouseInDetail> WarehouseInDetails { get; set; }
    }
}
