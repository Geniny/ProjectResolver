using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsResolver.Console.Models.Commands;
using ProjectsResolver.Console.Models.Properties;

namespace ProjectsResolver.Console.Models
{
    internal class ProjectsCommand : Command
    {
        public ProjectsCommand(string name, Action<List<CommandProperty>> action, List<CommandProperty> properties) : base(name, action, properties)
        {
            this.Properties.Add(new HelpProperty() { Description = "Вывести информацию о команде" });
            this.Properties.Add(new AllProperty() { Description = "Вывести все проекты решения" });

            if (properties != null && properties.Count() > 0)
                this.Properties.Concat(properties);
        }
    }
}
