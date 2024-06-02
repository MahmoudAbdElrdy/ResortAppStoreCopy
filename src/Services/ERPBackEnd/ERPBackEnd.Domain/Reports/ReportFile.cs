using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Reports
{
    public class ReportFile : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public string ReportNameAr { get; set; }
        public string? ReportNameEn { get; set; }
        public bool? IsDefault { get; set; }
        public int? ReportType { get; set; }
        public int? ReportTypeId { get; set; }
        public string FileName { get; set; }
        public bool? IsBasic { get; set; }

       


    }
}
