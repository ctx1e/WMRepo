namespace WMAPI.DTO
{
    public class GetWarehouseOutRequest
    {
        public string RecipientName { get; set; } = null!;
        public decimal TotalPriceOut { get; set; }
        public List<WarehouseOutDetailDTO> WarehouseOutDetailDTOs { get; set; } = new List<WarehouseOutDetailDTO>();

    }
}
