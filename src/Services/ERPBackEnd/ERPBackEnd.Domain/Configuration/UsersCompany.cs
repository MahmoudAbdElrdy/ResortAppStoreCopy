using Configuration.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class UsersCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
        public long? CompanyId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public virtual ICollection<UserCompaniesBranch> UserCompaniesBranchs { get; set; }  
    }
}
