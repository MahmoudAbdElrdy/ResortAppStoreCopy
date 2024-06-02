using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Configuration.Entities;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Projects.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configurations.Projects.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ProjectController : MainController<Project, ProjectDto, long>
    {
        private IProjectRepository _ProjectRepository { get; set; }
        public ProjectController(GMappRepository<Project, ProjectDto, long> mainRepos, IProjectRepository ProjectRepository) : base(mainRepos)
        {
            _ProjectRepository = ProjectRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<ProjectTreeDto>>> ShowTree([FromQuery] GetAllProjectTreeCommand query)
        {
            return Ok(await _ProjectRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(long? parentId)
        {
            return Ok(await _ProjectRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<ProjectDto>> Add([FromBody] ProjectDto command)
        {
            return Ok(await _ProjectRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<ProjectDto>> Edit([FromBody] ProjectDto command)
        {

            return Ok(await _ProjectRepository.Update(command));
        }
        [HttpGet("deleteProject")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<ProjectDto>> Delete(long id) 
        {
            return Ok(await _ProjectRepository.DeleteProject(new DeleteProjectCommand() { Id = id }));
        }
    }
}
