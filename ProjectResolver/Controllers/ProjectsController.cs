using Microsoft.AspNetCore.Mvc;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System.Xml.Linq;

namespace ProjectResolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet(Name = "getProjects")]
        public IEnumerable<Project> List()
        {
            return _projectService.List();
        }
    }
}
