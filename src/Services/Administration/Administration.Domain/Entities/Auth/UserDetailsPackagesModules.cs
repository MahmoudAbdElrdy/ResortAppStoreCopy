using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserDetailsPackagesModules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        
        public long UserDetailsModuleId { get; set; } 

   
        public long UserDetailsPackageId { get; set; } 

   
    }
}
