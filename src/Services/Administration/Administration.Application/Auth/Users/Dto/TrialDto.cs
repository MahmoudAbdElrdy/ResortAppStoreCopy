using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class TrialDto
    {
        public string username { get; set; }

        public long organizationId { get; set; }

        public string company { get; set; }

        public int moduleId { get; set; }
    }
}
