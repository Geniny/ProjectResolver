using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Properties;

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

    public CommandProperty(string name, PropertyType type)
    {
        this.Name = name;
        this.Type= type;
    }
}
