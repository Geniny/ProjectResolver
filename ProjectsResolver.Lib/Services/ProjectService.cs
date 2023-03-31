using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.Web.Administration;
using ProjectsResolver.Lib.Data;
using ProjectsResolver.Lib.Models;
using Configuration = ProjectsResolver.Lib.Models.Configuration;

namespace ProjectsResolver.Lib.Services
{
    public class ProjectService
    {
        public Solution Solution { get; set; }
        public Configuration Configuration { get; set; }

        public ProjectService(Solution solution, Configuration configuration)
        {
            this.Configuration = configuration;
            this.Solution = solution;
            new SolutionLoader(solution).Load(Solution.LocalUrl + "\\" + Solution.Name + ".sln");
            InitializeProjects();
        }

        public ProjectService Update()
        {
            InitializeProjects();
            return this;
        }

        public Project? Get(string name)
        {
            if (name != null)
            {
                var project = Solution.Projects
                    .Where(p => p.Name == name || p.NameAlias.ToLower() == name.ToLower())
                    .FirstOrDefault();

                return project;
            }

            return null;
        }

        public Models.Site Get(Project project)
        {
            Models.Site projectSite = null;
            using (ServerManager serverManager = new ServerManager())
            {
                var sites = serverManager.Sites;
                foreach (Microsoft.Web.Administration.Site site in sites)
                {
                    var appVirtualDir = site.Applications.SingleOrDefault();
                    var siteVirtualDir = appVirtualDir.VirtualDirectories.SingleOrDefault();
                    if (siteVirtualDir.PhysicalPath == project.PublishUrl)
                    {
                        var state = site.State == ObjectState.Started ? true : false;
                        var bindings = site.Bindings.FirstOrDefault();
                        projectSite = new Models.Site() { Name = site.Name, IsRunnig = state, Path = siteVirtualDir.PhysicalPath, Port = bindings.BindingInformation };
                        break;
                    }
                }
            }

            return projectSite;
        }

        public IEnumerable<Project> List()
        {
            return Solution.Projects;
        }

        private string LoadString(Func<XmlDocument, string> loader, string path)
        {
            string loadedValue = null;
            var file = new FileInfo(path);
            if (file.Exists)
            {
                var projectPublishProperties = new XmlDocument();
                projectPublishProperties.Load(path);
                loadedValue = loader.Invoke(projectPublishProperties);
            }

            return loadedValue;
        }

        private string GetAlias(string name)
        {
            StringBuilder alias = new StringBuilder();
            foreach (char symbol in name)
            {
                if (char.IsUpper(symbol))
                    alias.Append(symbol);
            }

            return alias.ToString();
        }

        private string GetBinding(string binding)
        {
            return binding;
        }

        private string LoadPublishVersion(string path)
        {
            string version = null;
            var file = new FileInfo(path);
            if (file.Exists)
            {
                var fileVersion = FileVersionInfo.GetVersionInfo(path);
                if (fileVersion != null)
                    version = fileVersion.FileVersion;
            }

            return version;
        }

        private void InitializeProjects()
        {
            foreach (var project in Solution.Projects)
            {
                var projectPath = Path.Combine(project.Url, project.Name + ".csproj");
                var publishPropertiesPath = Path.Combine(
                    project.Url,
                    "Properties",
                    "PublishProfiles",
                    $"{this.Configuration.PublishProfiles[Configuration.IsLocal ? "Local" : "Public"]}.pubxml"
                );

                var propertiesReader = new PropertiesReader();
                project.PublishUrl = LoadString(propertiesReader.ReadPublishUrl, publishPropertiesPath);
                project.Version = LoadString(propertiesReader.ReadVersion, projectPath);
                project.NameAlias = GetAlias(project.Name);

                if (!string.IsNullOrEmpty(project.PublishUrl))
                {
                    var publishedProjectExePath = Path.Combine(
                        project.PublishUrl,
                        project.Name + ".exe"
                    );
                    project.PublishVersion = LoadPublishVersion(publishedProjectExePath);
                    try
                    {
                        project.Site = this.Get(project);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
