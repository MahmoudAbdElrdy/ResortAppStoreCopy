using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Configuration.Entities
{
  public class UserRole : IdentityUserRole<string>
    {
        [Required, ForeignKey("UserId")]
        public User User { get; set; }

        [Required, ForeignKey("RoleId")]
        public Role Role { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

    }

}
