﻿using ProjectsResolver.Console.Models.Commands;
using ProjectsResolver.Console.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Handlers
{
    internal class CommandHandler
    {
        public Dictionary<string, Command>? commandDictionary
        {
            get;
        }
        public List<Command>? commands { get; set; }

        private bool isStart = false;

        public CommandHandler()
        {
            this.commandDictionary = new Dictionary<string, Command>();
            this.commands = new List<Command>();
        }

        public CommandHandler AddCommand(Command command)
        {
            this.commands.Add(command);
            this.commandDictionary.Add(command.Name, command);
            return this;
        }


        public void Start()
        {
            this.isStart = true;
            while (this.isStart)
            {
                System.Console.Write("> ");
                var input = System.Console.ReadLine();
                this.ReadCommand(input);
            }
        }
        public void Stop()
        {
            this.isStart = false;
        }

        public string CommandHelp(Command command)
        {
            var helpList = new StringBuilder();
            ;
            if (command == null)
            {
                return "No help info for this command";
            }

            foreach (var property in command.Properties)
            {
                helpList.AppendLine($"\t{command.Name} {property.Name}\t{property.Description}");
            }

            return helpList.ToString();
        }


        public void Execute(string command, List<string> properties)
        {
            if (string.IsNullOrEmpty(command))
                return;
            else
            {
                Command commandToExecute = null;
                commandDictionary.TryGetValue(command.ToLower(), out commandToExecute);
                if (commandToExecute != null)
                {
                    var propertiesToExecute = new List<CommandProperty>();
                    var argsHandler = new ArgumentsHandler();

                    if (properties == null)
                        commandToExecute.Action.Invoke(null);
                    else
                    {
                        foreach (var property in properties)
                        {
                            CommandProperty commandProperty = null;

                            if (argsHandler.IsValueProperty(property))
                            {
                                commandProperty = commandToExecute.Properties
                                    .Where(x => x.Type == PropertyType.Value)
                                    .FirstOrDefault();
                            }
                            else
                            {
                                commandProperty = commandToExecute.Properties
                                    .Where(x => x.Name.ToLower() == property.ToLower())
                                    .FirstOrDefault();
                            }

                            if (commandProperty == null)
                                throw new ArgumentException($"Can't execute {property}");

                            commandProperty.Value = property;
                            propertiesToExecute.Add(commandProperty);
                        }

                        commandToExecute.Action.Invoke(propertiesToExecute);
                    }
                }
                else
                    return;
            }
        }

        private void ReadCommand(string input)
        {
            var trimmedInput = input.Trim().Split(' ', 2);
            var command = trimmedInput[0];
            string properties = null;
            if (trimmedInput.Count() > 1)
            {
                properties = trimmedInput[1];
            }
            try
            {
                var splittedProperties = new ArgumentsHandler().SplitProperties(properties);
                this.Execute(command, splittedProperties);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
