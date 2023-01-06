using Newtonsoft.Json;

namespace api.git.server.Models.Account
{
    public class SshKeyModel
    {
        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
