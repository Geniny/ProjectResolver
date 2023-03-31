using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectsResolver.Lib.Models
{
    public class Solution
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string LocalUrl { get; set; }

        [JsonPropertyName("projects")]
        public IEnumerable<Project> Projects { get; set; }
    }
}
