using Common.Entity;
using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class Role : IdentityRole<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set ; }

        [MaxLength(500)]
        public string? NameAr { get; set; }
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
        public virtual ICollection<PermissionRole> PermissionRoles { set; get; }
        public virtual HashSet<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

    }

}
