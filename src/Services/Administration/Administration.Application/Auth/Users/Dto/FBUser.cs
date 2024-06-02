using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class FBUser
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("data_access_expire_at")]
        public long DataAccessExpireAt { get; set; }

        [JsonProperty("expire_at")]
        public long ExpireAt { get; set; }


        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("scopes")]
        public List<string>  Scopes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
