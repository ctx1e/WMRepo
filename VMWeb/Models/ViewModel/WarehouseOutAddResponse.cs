namespace VMWeb.Models.ViewModel
{
    public class WarehouseOutAddResponse
    {
        public string RecipientName { get; set; } = null!;
        public decimal TotalPriceOut { get; set; }
        public List<WarehouseOutDetail> WarehouseOutDetailDTOs { get; set; } = new List<WarehouseOutDetail>();
    }
}
