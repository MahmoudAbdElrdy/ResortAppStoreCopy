namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class CalculateItemCardBalanceDto 
    {
        public double? ConvertedPreviousBalance { get; set; }
        public double? ConvertedFirstPeriodQuantity { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }


    }
}
