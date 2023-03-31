using ConsoleTables;
using ProjectsResolver.Console;
using ProjectsResolver.Console.Handlers;
using ProjectsResolver.Console.Models;
using ProjectsResolver.Lib;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System.Text;
using System.Text.Json;

string solutionJson = string.Empty;
string configJson = string.Empty;


try
{
    solutionJson = new StreamReader("solution.json").ReadToEnd();
    configJson = new StreamReader("config.json").ReadToEnd();
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Can't find {ex.FileName}");
}

Configuration config = JsonSerializer.Deserialize<Configuration>(configJson);
Solution solution = JsonSerializer.Deserialize<Solution>(solutionJson);

if (args.Length > 0 && args[0] == "--local")
    config.IsLocal = true;
else if (args.Length > 0 && args[0] == "--public")
    config.IsLocal = false;

var projectService = new ProjectService(solution, config);
var commandHadler = new ProjectCommandHandler();
commandHadler.ProjectsCommand = Projects;
commandHadler.HelpCommand = Help;
commandHadler.PublishCommand = Publish;
commandHadler.ClearCommand = Clear;
commandHadler.HostsCommand = Hosts;

commandHadler.Start();

void Projects(List<CommandProperty> props)
{
    Console.WriteLine();
    var table = new ConsoleTable($"{config.PublishProfiles[config.IsLocal ? "Local" : "Public"]}", "Alias", "Started", "Local", "IIS", "");
    Predicate<Project> filter = null;
    if (props == null || props.Count() < 1)
        filter = (project) => project.PublishVersion != null;
    else if (props.Find(p => p.Name == "--help") != null)
    {
        Console.WriteLine(commandHadler.CommandHelp("projects"));
        return;
    }
    else if (props.Find(p => p.Name == "--all") != null)
        filter = (project) => true;

    foreach (var project in projectService.List().Where(p => filter(p)))
    {
        table.AddRow(project.Name, project.NameAlias, project.Site != null ? project.Site.IsRunnig : false, project.Version, project.PublishVersion, project.Version == project.PublishVersion ? "+" : "-");
    }

    table.Write(Format.Alternative);
}

void Hosts(List<CommandProperty> props)
{
    Predicate<Site> filter = null;
    if (props == null || props.Count() < 1)
        filter = (site) => true;
    else if (props.Find(p => p.Name == "--help") != null)
    {
        Console.WriteLine("\n" + commandHadler.CommandHelp("hosts"));
        return;
    }
    else if (props.Find(p => p.Name == "--started") != null)
        filter = (site) => site.IsRunnig == true;
    else if (props.Find(p => p.Name == "--stopped") != null)
        filter = (site) => site.IsRunnig == false;


    var table = new ConsoleTable("Site", "Path", "Started", "Port");
    foreach(var project in projectService.List().Where(p => p.Site != null))
    {
        if(filter(project.Site))
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
    if (props == null || props.Find(p => p.Name == "--help") != null)
    {
        Console.WriteLine(commandHadler.CommandHelp("publish"));
        return;
    }
    else if (props.Find(p => p.Name == "--all") != null)
        await foreach (var publishResult in new Resolver().PublishAsync(projectService.Solution))
        {
            if (!string.IsNullOrEmpty(publishResult))
                Console.WriteLine(publishResult);
        }
    else
    {
        var valueProperty = props.Find(p => p.Type == PropertyType.Value);
        if (valueProperty != null)
        {
            var project = projectService.Get(valueProperty.Value);
            if (project != null)
                Console.WriteLine(await new Resolver().PublishAsync(project, true));
            else
            {
                Console.WriteLine($"Can't found {valueProperty.Value} project");
            }

        }
    }

    projectService.Update();
    commandHadler.Execute("projects", null);
}

void Clear(List<CommandProperty> props)
{
    Console.Clear();
}

void Help(List<CommandProperty> messages)
{
    Console.WriteLine();
    var helpOutput = new StringBuilder();
    foreach (var com in commandHadler.commandDictionary)
    {
        helpOutput.AppendLine($"{com.Key} command");
        helpOutput.AppendLine(commandHadler.CommandHelp(com.Key));
    }

    System.Console.Write(helpOutput.ToString());
}
