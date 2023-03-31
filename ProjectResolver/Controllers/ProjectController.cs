using Microsoft.AspNetCore.Mvc;
using ProjectsResolver.Lib.Models;
using ProjectsResolver.Lib.Services;
using System.Xml.Linq;

namespace ProjectResolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet(Name = "getProject/{name}")]
        public Project GetProject(string name)
        {
            return _projectService.Get(name);
        }
    }
}
