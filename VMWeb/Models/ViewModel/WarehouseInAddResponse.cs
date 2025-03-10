namespace VMWeb.Models.ViewModel
{
    public class WarehouseInAddResponse
    {

        public string SupplierName { get; set; } = null!;
        public decimal TotalPriceIn { get; set; }
        public List<WarehouseInDetail> WarehouseInDetailDTOs { get; set; } = new List<WarehouseInDetail>();

    }
}
