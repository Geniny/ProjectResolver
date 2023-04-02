using ConsoleTables;
using ProjectsResolver.Console;
using ProjectsResolver.Console.Handlers;
using ProjectsResolver.Console.Models;
using ProjectsResolver.Console.Models.Commands;
using ProjectsResolver.Console.Models.Properties;
using ProjectsResolver.Lib;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System.Text;
using System.Text.Json;

var application = new App(args);

application
    .AddSolution("solution.json")
    .AddConfig("config.json")
    .AddServices()
    .AddCommands(new List<Command>() {
        new PublishCommand("publish", Publish, null),
        new ProjectsCommand("projects", Projects, null),
        new HostsCommand("hosts", Hosts, null),
        new HelpCommand("help", Help, null),
        new ClearCommand("clear", Clear, null),
        new ExitCommand("exit", Exit, null)

    });

void Projects(List<CommandProperty> props)
{
    Console.WriteLine();
    var command = application.CommandHandler.commands.FirstOrDefault(c => c is ProjectsCommand);
    var table = new ConsoleTable($"{application.configuration.PublishProfiles[application.configuration.IsLocal ? "Local" : "Public"]}", "Alias", "Started", "Local", "IIS", "");
    Predicate<Project> filter = null;

    if (props == null || props.Count() < 1)
        filter = (project) => project.PublishVersion != null;
    else if (props.Find(p => p is HelpProperty) != null)
    {
        Console.WriteLine(application.CommandHandler.CommandHelp(command));
        return;
    }
    else if (props.Find(p => p is AllProperty) != null)
        filter = (project) => true;

    foreach (var project in application.ProjectService.List().Where(p => filter(p)))
    {
        table.AddRow(project.Name, project.NameAlias, project.Site != null ? project.Site.IsRunnig : false, project.Version, project.PublishVersion, project.Version == project.PublishVersion ? "+" : "-");
    }

    table.Write(Format.Alternative);
}

void Hosts(List<CommandProperty> props)
{
    var command = application.CommandHandler.commands.FirstOrDefault(c => c is HostsCommand);
    Predicate<Site> filter = null;
    if (props == null || props.Count() < 1)
        filter = (site) => true;
    else if (props.Find(p => p is HelpProperty) != null)
    {
        Console.WriteLine("\n" + application.CommandHandler.CommandHelp(command));
        return;
    }
    else if (props.Find(p => p is StartedProperty) != null)
        filter = (site) => site.IsRunnig == true;
    else if (props.Find(p => p is StoppedProperty) != null)
        filter = (site) => site.IsRunnig == false;


    var table = new ConsoleTable("Site", "Path", "Started", "Port");
    foreach (var project in application.ProjectService.List().Where(p => p.Site != null))
    {
        if (filter(project.Site))
        {
            table.AddRow(project.Site.Name, project.Site.Path, project.Site.IsRunnig, project.Site.Port);
        }
    }

    Console.WriteLine();
    table.Write(Format.Alternative);

    return;
}

async void Publish(List<CommandProperty> props)
{
    Console.WriteLine();
    var command = application.CommandHandler.commands.FirstOrDefault(x => x is PublishCommand);

    if (props == null || props.Find(p => p is HelpProperty) != null)
    {
        Console.WriteLine(application.CommandHandler.CommandHelp(command));
        return;
    }
    else if (props.Find(p => p is AllProperty) != null)
        await foreach (var publishResult in new Resolver().PublishAsync(application.ProjectService.Solution))
        {
            if (!string.IsNullOrEmpty(publishResult))
                Console.WriteLine(publishResult);
        }
    else
    {
        var valueProperty = props.Find(p => p is ValueProperty);
        if (valueProperty != null)
        {
            var project = application.ProjectService.Get(valueProperty.Value);
            if (project != null)
                Console.WriteLine(await new Resolver().PublishAsync(project, true));
            else
            {
                Console.WriteLine($"Can't found {valueProperty.Value} project");
            }

        }
    }

    application.ProjectService.Update();
    application.CommandHandler.Execute("projects", null);
}

void Clear(List<CommandProperty> props)
{
    Console.Clear();
}

void Help(List<CommandProperty> messages)
{
    Console.WriteLine();
    var helpOutput = new StringBuilder();
    foreach (var com in application.CommandHandler.commandDictionary)
    {
        helpOutput.AppendLine($"{com.Key} command");
        helpOutput.AppendLine(application.CommandHandler.CommandHelp(com.Value));
    }

    System.Console.Write(helpOutput.ToString());
}

void Exit (List<CommandProperty> props)
{
    application.CommandHandler.Stop();
}
