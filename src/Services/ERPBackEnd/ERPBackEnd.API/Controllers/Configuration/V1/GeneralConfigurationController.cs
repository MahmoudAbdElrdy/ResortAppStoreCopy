using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class GeneralConfigurationController : BaseController
    {
        private IGeneralConfigurationsRepository _repository;
        public GeneralConfigurationController(
            GMappRepository<GeneralConfiguration, GeneralConfigurationDto, long> mainRepos, IMapper mapper,
            IGeneralConfigurationsRepository repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        [HttpGet("show")]
        public async Task<ActionResult<PageList<GeneralConfigurationDto>>> Show([FromQuery] GetAllGeneralConfigurationWithPagination query)
        {
            return Ok((_repository.GetAllGeneralConfigurationWithPaginatio(query)));
        }
        [HttpPost("edit")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<GeneralConfigurationDto>> Edit([FromBody] Application.Configuration.GeneralConfigurations.Dto.EditGeneralConfigurationCommand command)
        {

            return Ok((await _repository.EditGeneralConfiguration(command)));
        }
        [HttpGet("getById")]

        public async Task<ActionResult<GeneralConfigurationDto>> GetById(int id)
        {
            return Ok(await _repository.GetById(id));
        }
    }
}
