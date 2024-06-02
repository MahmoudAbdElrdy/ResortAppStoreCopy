namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Dto
{
    public class ReservedTablesDto
    {

        public long Id { get; set; }
        public string TableNameAr { get; set; }
        public string TableNameEn { get; set; }
        public int NumberOfSeats { get; set; }
        public long BillId { get; set; }
        public long BillTypeId { get; set; }



    }
}
