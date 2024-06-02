using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class ResponseDto
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public ResponseDetail ResponseDetail { get; set; }
        public bool Success { get; set; }
    }
}
