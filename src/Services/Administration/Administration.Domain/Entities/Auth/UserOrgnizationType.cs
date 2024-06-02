using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserOrgnizationType
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public long OrganizationId { get; set; }

        public UserType UserType { get; set; }


    }
}
