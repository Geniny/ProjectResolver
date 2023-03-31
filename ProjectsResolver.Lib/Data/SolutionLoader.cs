using ProjectsResolver.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectsResolver.Lib.Data
{
    internal class SolutionLoader
    {
        public Solution Solution { get; set; }

        public SolutionLoader(Solution solution)
        {
            this.Solution = solution;
        }

        public void Load(string solutionUrl)
        {
            var Content = File.ReadAllText(solutionUrl);
            Regex projReg = new Regex(
                "Project\\(\"\\{[\\w-]*\\}\"\\) = \"([\\w _]*.*)\", \"(.*\\.(cs|vcx|vb)proj)\"",
                RegexOptions.Compiled
            );
            var matches = projReg.Matches(Content).Cast<Match>();
            var Projects = matches
                .Select(
                    x => new Project() { Name = x.Groups[1].Value, LocalUrl = x.Groups[2].Value }
                )
                .ToList();
            foreach (var project in Projects)
            {
                var localPath = project.LocalUrl;
                project.LocalUrl = Path.GetDirectoryName(localPath);
                var fullPath = "";
                if (!Path.IsPathRooted(localPath))
                    fullPath = Path.Combine(
                        Path.GetDirectoryName(solutionUrl),
                        project.LocalUrl
                    );

                project.Url = Path.GetFullPath(fullPath);
            }

            this.Solution.Projects = Projects;
        }
    }
}
