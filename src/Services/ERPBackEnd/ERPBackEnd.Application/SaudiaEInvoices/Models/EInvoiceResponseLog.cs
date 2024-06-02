using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Models
{
    public class EInvoiceResponseLog
    {
        [Key]
        public long Id { get; set; }
        public Guid? InvGuid { get; set; }
        public string? ApiResponse { get; set; }
        public int? StatusCode { get; set; }
        public string? Operation { get; set; }
        public DateTime OperationDate { get; set; }
        public string? ExtApiUrl { get; set; }
    }
}
