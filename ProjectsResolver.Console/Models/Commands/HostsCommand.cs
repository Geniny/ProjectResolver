using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsResolver.Console.Models.Commands;
using ProjectsResolver.Console.Models.Properties;

namespace ProjectsResolver.Console.Models
{
    internal class HostsCommand : Command
    {
        public HostsCommand(string name, Action<List<CommandProperty>> action, List<CommandProperty> properties) 
            : base(name, action, properties)
        {
            this.Properties.Add(new HelpProperty() { Description = "Вывести информацию о команде" });
            this.Properties.Add(new StartedProperty() { Description = "Вывести запущенные хосты" });
            this.Properties.Add(new StoppedProperty() { Description = "Вывести остановленные хосты" });

            if (properties != null && properties.Count() > 0)
                this.Properties.Concat(properties);
        }
    }
}
