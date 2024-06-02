using Administration.API.Helpers;
using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Settings.GeneralConfigurations.Dto;
using ResortAppStore.Services.Administration.Application.Settings.Settings.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SettingController : BaseController
    {
        private ISettingsRepository _repository;
        public SettingController(
            GMappRepository<Setting, SettingDto, long> mainRepos, IMapper mapper,
            ISettingsRepository repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        [HttpGet("getAllSetting")]
        public async Task<ActionResult<PageList<SettingDto>>> GetAllSetting() 
        {
            return Ok((await _repository.GetAllSetting()));
        }
        [HttpPost("edit")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<SettingDto>> Edit([FromBody] EditSettingDto command)
        {

            return Ok((await _repository.EditSetting(command)));
        }
        [HttpGet("getById")]

        public async Task<ActionResult<SettingDto>> GetById(int id)
        {
            return Ok(await _repository.GetById(id));
        }
    }
}
