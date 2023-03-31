using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectsResolver.Lib.Models
{
    public class Configuration
    {
        [JsonPropertyName("local")]
        public bool IsLocal { get; set; }

        [JsonPropertyName("publishProfiles")]
        public Dictionary<string, string> PublishProfiles { get; set; }
    }
}
