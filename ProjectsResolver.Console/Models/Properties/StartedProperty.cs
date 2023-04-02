using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Properties
{
    internal class StartedProperty : CommandProperty
    {
        public StartedProperty() : base("--started", PropertyType.Single)
        {
        }
    }
}
