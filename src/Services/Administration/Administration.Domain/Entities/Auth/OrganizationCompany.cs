
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class OrganizationCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public long Id { get; set; }
        
        public string CompanyNameAr { get; set; }

        public string CompanyNameEn { get; set; }

        public int NumberOfBranches { get; set; }

        public bool IsExtra { get; set; }

        public long OrganizationId { get; set; }

    }
}
