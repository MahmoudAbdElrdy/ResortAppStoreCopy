using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Models
{
    public class ElectBill
    {
        [Key]
        public Guid Guid { get; set; }
        public Guid BillGuid { get; set; }
        public string? BillData { get; set; }
        public string? URL { get; set; }

    }
}
