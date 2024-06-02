using Common.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Subscription
{
    public class Package : BaseTrackingEntity<long>
    {
        public Package()
        {
            MonthlyPrice = default;
            YearlyPrice = default;
            FullBuyPrice = default;
            PackagesModules = new HashSet<PackagesModules>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(1000)]
        public string NameAr { get; set; }

        [MaxLength(1000)]
        public string NameEn { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal  MonthlyPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal YearlyPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FullBuyPrice { get; set; }

        public int NumberOfUsers { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }

        public int? BillPattrenNumber { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public ICollection<PackagesModules> PackagesModules { get; set; }

        public ICollection<UserDetailsPackage> UserDetailsPackages { get; set; } 
    }
}
