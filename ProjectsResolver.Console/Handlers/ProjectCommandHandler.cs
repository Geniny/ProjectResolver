using ProjectsResolver.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsResolver.Console.Handlers
{
    internal class ProjectCommandHandler : CommandHandler
    {
        public Action<List<CommandProperty>> AddCommand { get; set; }
        public Action<List<CommandProperty>> ProjectsCommand { get; set; }
        public Action<List<CommandProperty>> PublishCommand { get; set; }
        public Action<List<CommandProperty>> HostsCommand { get; set; }


        public ProjectCommandHandler()
            : base()
        {
            var valueProperty = new CommandProperty()
            {
                Name = "[VALUE]",
                Type = PropertyType.Value
            };

            var publishValueProperty = new CommandProperty()
            {
                Name = "[PROJECT | ALIAS]",
                Description = "Publish project by name or alias",
                Type = PropertyType.Value
            };

            var helpProperty = new CommandProperty()
            {
                Name = "--help",
                Description = "List available proprties for command and usage of them",
                Type = PropertyType.Single
            };

            var allProjectsProperty = new CommandProperty()
            {
                Name = "--all",
                Description = "list all projects",
                Type = PropertyType.Single
            };

            var publishAllProperty = new CommandProperty()
            {
                Name = "--all",
                Description = "publish all available projects in solution",
                Type = PropertyType.Single
            };

            var startedProperty = new CommandProperty()
            {
                Name = "--started",
                Description = "list all hosts with started state",
                Type = PropertyType.Single
            };

            var stoppedProperty = new CommandProperty()
            {
                Name = "--stopped",
                Description = "list all hosts with stopped state",
                Type = PropertyType.Single
            };

            var projectsCommand = new Command(
                "projects",
                (props) => this.ProjectsCommand(props),
                new List<CommandProperty>() { allProjectsProperty, helpProperty }
            );

            var publishCommand = new Command(
                "publish",
                (props) => this.PublishCommand(props),
                new List<CommandProperty>()
                {
                    publishAllProperty,
                    publishValueProperty,
                    helpProperty
                }
            );

            var clearCommand = new Command(
                "clear",
                (props) => this.ClearCommand(props),
                new List<CommandProperty>() { }
            );

            var exitCommand = new Command(
                "exit",
                (props) => this.ExitCommand(props),
                new List<CommandProperty>() { }
            );

            var helpCommand = new Command(
                "help",
                (props) => this.HelpCommand(props),
                new List<CommandProperty>() { }
            );

            var hostsCommand = new Command(
                "hosts",
                (props) => this.HostsCommand(props),
                new List<CommandProperty>() {startedProperty, stoppedProperty, helpProperty }
            );

            var commands = new List<Command>()
            {
                projectsCommand,
                publishCommand,
                clearCommand,
                exitCommand,
                helpCommand,
                hostsCommand
            };

            this.commandDictionary = commands.ToDictionary(x => x.Name, x => x);
        }
    }
}
