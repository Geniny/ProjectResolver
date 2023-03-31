using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectsResolver.Lib.Models
{
    public class Site
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isRunnig")]
        public bool IsRunnig { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}
