using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models
{
    enum PropertyType
    {
        Value,
        Single,
        Paired
    }

    internal class CommandProperty
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public string Value { get; set; }
    }
}
