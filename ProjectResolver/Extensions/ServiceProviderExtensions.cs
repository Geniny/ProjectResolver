using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System.Text.Json;

namespace ProjectResolver.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddProjectService(this IServiceCollection services)
        {
            string solutionJson = string.Empty;
            string configJson = string.Empty;

            try
            {
                solutionJson = new StreamReader("solution.json").ReadToEnd();
                configJson = new StreamReader("config.json").ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                //logger.Log(LogLevel.Error, $"Can't find {ex.FileName}");
            }

            Configuration config = JsonSerializer.Deserialize<Configuration>(configJson);
            Solution solution = JsonSerializer.Deserialize<Solution>(solutionJson);

            services.AddTransient<ProjectService>(service => new ProjectService(solution, config));
        }
    }
}
