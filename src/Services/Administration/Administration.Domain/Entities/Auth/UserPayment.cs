using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }

        public long? UserPackageId { get; set; }

        public long? UserModuleCode { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCancelled { get; set; }
    }
}
