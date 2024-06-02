using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
  

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AudienceError
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
    }

  

    public class Error
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
        public SharedLinkAlreadyExists shared_link_already_exists { get; set; }
    }

    public class LinkPermissionsError
    {
        public ResolvedVisibility resolved_visibility { get; set; }
        public RequestedVisibility requested_visibility { get; set; }
        public bool can_revoke { get; set; }
        public List<VisibilityPolicy> visibility_policies { get; set; }
        public bool can_set_expiry { get; set; }
        public bool can_remove_expiry { get; set; }
        public bool allow_download { get; set; }
        public bool can_allow_download { get; set; }
        public bool can_disallow_download { get; set; }
        public bool allow_comments { get; set; }
        public bool team_restricts_comments { get; set; }
        public List<AudienceOption> audience_options { get; set; }
        public bool can_set_password { get; set; }
        public bool can_remove_password { get; set; }
    }

    public class Metadata
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string path_lower { get; set; }
        public LinkPermissions link_permissions { get; set; }
        public string preview_type { get; set; }
        public DateTime client_modified { get; set; }
        public DateTime server_modified { get; set; }
        public string rev { get; set; }
        public int size { get; set; }
    }

   




    public class DropboxShareLinkErrorResponse
    {
        public string error_summary { get; set; }
        public Error error { get; set; }
    }

    public class SharedLinkAlreadyExists
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
        public Metadata metadata { get; set; }
    }

   


}
