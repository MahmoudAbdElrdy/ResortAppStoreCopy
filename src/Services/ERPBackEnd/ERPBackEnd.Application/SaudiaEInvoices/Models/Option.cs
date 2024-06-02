using System;
using System.Collections.Generic;

namespace SaudiEinvoiceService.Models
{
    public partial class Option
    {
        public Guid Guid { get; set; }
        public int? UserId { get; set; }
        public string? Type { get; set; }
        public string? StrVal { get; set; }
        public DateTime? DateVal { get; set; }
        public double? NumVal { get; set; }
        public string? Section { get; set; }
        public string? ComputerName { get; set; }
        public bool? IsSec { get; set; }
        public string? TextVal { get; set; }
        public string? StrVal2 { get; set; }
        public string? StrVal3 { get; set; }
    }
}
