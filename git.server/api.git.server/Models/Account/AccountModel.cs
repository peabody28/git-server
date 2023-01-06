using Newtonsoft.Json;

namespace api.git.server.Models.Account
{
    public class AccountModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
