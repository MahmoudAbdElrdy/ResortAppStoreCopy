using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Shared.Dto
{
    public class ArchiveFiles
    {
        public string Caption { get; set; }
        public long Size { get; set; }
        public int Key { get; set; }
        public string FileType { get; set; }
    }
}
