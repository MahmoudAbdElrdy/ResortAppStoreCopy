using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Subscription;
using ResortAppStore.Services.Administration.Domain;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ModuleController :  MainController<Module, ModuleDto, long>
    {
        private IModuleRepo _repositoryModule;
        public ModuleController(GMappRepository<Module, ModuleDto, long> mainRepos, 
            IModuleRepo repositoryModule) : base(mainRepos)
        {
            _repositoryModule = repositoryModule;
        }

        [HttpGet("getModuleById/{id}")]
        public async Task<ActionResult<ModuleDto>> GetModuleById(long id)
        {
            return Ok(await _repositoryModule.GetModuleById(id));
        }

        [AllowAnonymous]
        [HttpGet("GetModules")]
        public async Task<ActionResult<List<ModuleDto>>> GetModules()
        {
            return Ok((await _repositoryModule.GetAllModule()));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public  async Task<ActionResult<ModuleDto>> Update([FromBody] EditModuleCommand input)
        {
            return Ok(await _repositoryModule.EditModule(input));
        }
    }
}
