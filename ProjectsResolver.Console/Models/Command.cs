using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models
{
    internal class Command
    {
        public string Name { get; set; }
        public Action<List<CommandProperty>> Action { get; set; }
        public List<CommandProperty> Properties { get; set; }

        public Command(
            string name,
            Action<List<CommandProperty>> action,
            List<CommandProperty> properties
        )
        {
            this.Action = action;
            this.Name = name;
            this.Properties = properties;
        }
    }
}
