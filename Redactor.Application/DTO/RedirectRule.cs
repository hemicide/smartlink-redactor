using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor.Application.DTO
{
    public class RedirectRule
    {
        [JsonProperty("predicates", Required = Required.Always)]
        public string[] Predicates;

        [JsonProperty("args", Required = Required.Always)]
        public IDictionary<string, object> Args;

        [JsonProperty("redirectTo", Required = Required.Always)]
        public string RedirectTo;
    }
}
