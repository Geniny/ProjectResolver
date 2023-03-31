using Microsoft.Web.Administration;
using ProjectsResolver.Lib.Data;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Pipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace ProjectsResolver.Lib
{
    public class Resolver
    {
        public async Task<string> PublishAsync(Project project, bool singlePublish = false)
        {
            if (
                project != null
                && !string.IsNullOrEmpty(project.Url)
                && !string.IsNullOrEmpty(project.PublishUrl)
            )
            {
                if (project.PublishVersion!= null || singlePublish) 
                {
                    if (project.Version != project.PublishVersion && project.PublishUrl != null)
                    {
                        var publishCommand = $"/C dotnet publish {project.Url} -o {project.PublishUrl}";
                        return new CommandLinePipeline().Execute(publishCommand);
                    }

                    return $"{project.Name} is up to date";
                }
            }

            return null;
        }

        public async IAsyncEnumerable<string> PublishAsync(Solution solution)
        {
            if (solution != null && solution.Projects.Count() > 0)
            {
                foreach (var project in solution.Projects)
                {
                    yield return await this.PublishAsync(project);
                }
            }
        }
    }
}
