using Newtonsoft.Json;

namespace Redactor.Application.DTO
{
    [Serializable]
    public class LinkResponse
    {
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }

        [JsonProperty("link", Required = Required.Always)]
        public string Link { get; set; }

        [JsonProperty("rules", Required = Required.Always)]
        public IEnumerable<RedirectRule> Rules { get; set; }
    }
}
