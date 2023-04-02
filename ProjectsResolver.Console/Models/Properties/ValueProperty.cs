using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Properties
{
    internal class ValueProperty : CommandProperty
    {
        public ValueProperty(string name) : base(name, PropertyType.Value)
        {
        }
    }
}
