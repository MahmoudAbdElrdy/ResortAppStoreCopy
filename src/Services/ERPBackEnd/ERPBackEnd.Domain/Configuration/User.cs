
using Common.Entity;
using Common.Enums;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class User : IdentityUser<string>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [MaxLength(500)]
        public string? NameAr { get; set; }
        [MaxLength(500)]
        public string? NameEn { get; set; }
        [MaxLength(500)]
        public string? FullName { get; set; }
        public string? Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(36)]
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(36)]
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        [MaxLength(300)]
        public string? Notes { get; set; }
        public HashSet<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string? Token { get; set; }
        public string? WebToken { get; set; }
        public bool? IsVerifyCode { get; set; }
        public bool? IsAddPassword { get; set; }
        public UserType? UserType { get; set; }
        public int? LoginCount { get; set; }
        public virtual ICollection<UsersCompany> UsersCompanies { get; set; } 


    }
}