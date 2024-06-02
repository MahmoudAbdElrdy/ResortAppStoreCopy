using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class UserCompaniesBranch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey(nameof(UsersCompanyId))]
        public UsersCompany? UsersCompany { get; set; }
        public long? UsersCompanyId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }
        public long? BranchId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
