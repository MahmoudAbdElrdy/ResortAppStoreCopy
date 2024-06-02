using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class PackageModuleCompany
    {
        public long Id { get; set; }

        public long? PackageUserDetailsId { get; set; }

        public long? ModuleUserDetailsCode { get; set; }

        public long CompanyId { get; set; }
    }
}
