using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Subscription
{
    public class PackagesModules
    {

      

            [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Module")]
        public long ModuleId { get; set; }

        [ForeignKey("Package")]
        public long PackageId { get; set; }

        public Module Module { get; set; }

        public Package Package { get; set; }
    }
}
