using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class Company : BaseTrackingEntity<long>
    {
        public Company()
        {
            MotherCompany = false;
            UseHijri = false;
        }

        [MaxLength(60)]
        public string NameAr { get; set; }
        [MaxLength(60)]
        public string? NameEn { get; set; }


        [MaxLength(500)]
        public string? Address { get; set; }
        [MaxLength(500)]
        public string? MailBox { get; set; }
        [MaxLength(100)]
        public string? ZipCode { get; set; }
       
        [MaxLength(100)]
        public string? CommercialRegistrationNo { get; set; }
        [MaxLength(100)]
        public string? TaxNumber { get; set; }
        [MaxLength(50)]
        public string? MobileNumber { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }
   

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? WebSite { get; set; } 
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
        public long? CountryId { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public Currency? Currency { get; set; }
        public long? CurrencyId { get; set; } 
        public bool? MotherCompany { get; set; }
        public bool? UseHijri { get; set; } 
        public string? Logo { get; set; }

        [MaxLength(255)]
        public string? SpecialNumber { get; set; }
        [MaxLength(255)]
        public string? Activity { get; set; }
        public byte? IntegrationType { get; set; }
        [MaxLength(50)]
        public string? BillType { get; set; }
        public string? GenCSRConfig { get; set; }
        public string? CSRBase64 { get; set; }
        public string? Certificate { get; set; }
        public string? SecretKey { get; set; }
        [MaxLength(255)]
        public string? RequestId { get; set; }
        [MaxLength(255)]
        public string? PCSID { get; set; }
        public string? ProductionSecretKey { get; set; }
        [MaxLength(255)]
        public string? ProductionRequestId { get; set; }
        public string? PublicKey { get; set; }
        public string? PrivateKey { get; set; }
        [MaxLength(1)]
        public string? CompanyType { get; set; }

        [MaxLength(50)]
        public string? Governorate { get; set; }

        [MaxLength(250)]
        public string? RegionCity { get; set; }

        [MaxLength(250)]
        public string? Street { get; set; }

        [MaxLength(10)]
        public string? BuildingNumber { get; set; }

        [MaxLength(50)]
        public string? Floor { get; set; }

        [MaxLength(50)]
        public string? Room { get; set; }

        [MaxLength(50)]
        public string? LandMark { get; set; }

        [MaxLength(250)]
        public string? AdditionalInformation { get; set; }

        [MaxLength(250)]
        public string? ClientId { get; set; }

        [MaxLength(250)]
        public string? ClientSecret { get; set; }

        public int? NumberOfBranches { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public long? OrganizationId { get; set; }

    }
}
