using Newtonsoft.Json;

namespace api.git.server.Models.GitRepository
{
    public class GitRepositoryModel
    {
        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
