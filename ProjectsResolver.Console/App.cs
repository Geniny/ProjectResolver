using ProjectsResolver.Console.Handlers;
using ProjectsResolver.Console.Models.Commands;
using ProjectsResolver.Console.Models;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectsResolver.Console
{
    internal class App
    {
        public Solution solution { get; private set; }
        public Configuration configuration { get; private set; }
        private string[] args;

        public ProjectService ProjectService { get; set; }
        public CommandHandler CommandHandler { get; private set; }

        public App(string[] args) 
        {
            this.args= args;
        }
        
        public App AddSolution (string solutionJsonPath)
        {
            var solutionJson = string.Empty;

            try
            {
                solutionJson = new StreamReader(solutionJsonPath).ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                System.Console.WriteLine($"Can't find {ex.FileName}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Ошибка при чтении файла:\n{ex.Message}");
            }

            this.solution = JsonSerializer.Deserialize<Solution>(solutionJson);
            return this;
        }

        public App AddConfig(string configJsonPath)
        {
            var configJson = string.Empty;

            try
            {
                configJson = new StreamReader(configJsonPath).ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                System.Console.WriteLine($"Can't find {ex.FileName}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Ошибка при чтении файла:\n{ex.Message}");
            }

            this.configuration = JsonSerializer.Deserialize<Configuration>(configJson);

            if (args.Length > 0 && args[0] == "--local")
                this.configuration.IsLocal = true;
            else if (args.Length > 0 && args[0] == "--public")
                this.configuration.IsLocal = false;

            return this;
        }

        public App AddServices ()
        {
            if (configuration != null && solution != null)
            {
                try
                {
                    this.ProjectService = new ProjectService(solution, configuration);
                }
                catch (FileNotFoundException ex)
                {
                    System.Console.WriteLine($"Неверный путь к решению / решения не найдено {ex.FileName}");
                }
            }

            return this;
        }

        public App AddCommands(List<Command> commands)
        {
            this.CommandHandler = new CommandHandler();
            foreach (var command in commands)
            {
                this.CommandHandler.AddCommand(command);
            }

            this.CommandHandler.Start();
            return this;
        }
    }
}
