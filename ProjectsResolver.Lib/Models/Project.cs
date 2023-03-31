using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectsResolver.Lib.Models
{
    public class Project
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alias")]
        public string NameAlias { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("currentVersion")]
        public string PublishVersion { get; set; }

        [JsonIgnore]
        public string PublishUrl { get; set; }

        [JsonIgnore]
        public string LocalUrl { get; set; }

        [JsonIgnore]
        public string Url { get; set; }

        [JsonIgnore]
        public string PublishProfileUrl { get; set; }

        [JsonPropertyName("site")]
        public Site Site { get; set; }
    }
}
