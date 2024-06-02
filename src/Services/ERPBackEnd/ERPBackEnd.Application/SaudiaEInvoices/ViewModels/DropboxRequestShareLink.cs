using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{


    public class DropboxRequestShareLink
    {
        public string path { get; set; }
        public ShareLinkSettings settings { get; set; }
    }
    public class ShareLinkSettings
    {
        public ShareLinkAccess access { get; set; }
    }
    public class ShareLinkAccess
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
    }

    

   

}
