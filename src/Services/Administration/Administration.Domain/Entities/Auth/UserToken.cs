using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? Expiry { get; set; }
        public DateTime? LastActivity { get; set; } 
        public bool IsValid { get; set; } = true;
    }
}
