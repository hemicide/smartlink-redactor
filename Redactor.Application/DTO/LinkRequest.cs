
using Newtonsoft.Json;

namespace Redactor.Application.DTO
{
    [Serializable]
    public class LinkRequest
    {
        [JsonProperty("link", Required = Required.Always)]
        public string Link { get; set; }

        [JsonProperty("rules", Required = Required.Always)]
        public IEnumerable<RedirectRule> Rules { get; set; }
    }
}
