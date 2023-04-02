using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Properties
{
    internal class AllProperty : CommandProperty
    {
        public AllProperty() : base("--all", PropertyType.Single)
        {
        }
    }
}
