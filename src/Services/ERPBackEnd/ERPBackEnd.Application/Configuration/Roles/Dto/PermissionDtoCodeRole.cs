
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Auth.Roles.Dto
{
    public class PermissionDtoCodeRole
    {
        public List<GetAllPermissionDTO> Permissions { get; set; }
        public List<ScreenDto> Screens { get; set; }
        public string Code { get; set; }
    }
}
