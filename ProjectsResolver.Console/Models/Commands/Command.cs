using ProjectsResolver.Console.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Commands
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
            Action = action;
            Name = name;
            Properties = new List<CommandProperty>();
            this.Properties.Add(new HelpProperty() { Description = "Вывести информацию о команде" });

            if (properties != null && properties.Count() > 0)
                this.Properties.Concat(properties);
            ;
        }
    }
}
