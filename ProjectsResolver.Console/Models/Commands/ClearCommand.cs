﻿using ProjectsResolver.Console.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Models.Commands
{
    internal class ClearCommand : Command
    {
        public ClearCommand(string name, Action<List<CommandProperty>> action, List<CommandProperty> properties) : base(name, action, properties)
        {
        }
    }
}
