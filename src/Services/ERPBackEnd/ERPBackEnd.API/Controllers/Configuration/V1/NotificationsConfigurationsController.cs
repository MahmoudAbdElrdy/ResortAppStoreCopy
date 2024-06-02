using AutoMapper;
using Common.BaseController;
using Common.Constants;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Configuration.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]

    public class NotificationsConfigurationsController : MainController<NotificationConfiguration, NotificationConfigurationDto, long>
    {
        private INotificationConfigurationRepository _repository;

        public NotificationsConfigurationsController(
            GMappRepository<NotificationConfiguration, NotificationConfigurationDto, long> mainRepos, IMapper mapper,
            INotificationConfigurationRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        //[HttpPost("create")]
        //[SuccessResultMessage("createSuccessfully")]
        public override async Task<NotificationConfigurationDto> Create([FromBody] NotificationConfigurationDto input)
        {
            return await _repository.CreateNotificationConfiguration(input);
        }
        //[HttpPost("update")]
        //[SuccessResultMessage("editSuccessfully")]
        public override async Task<NotificationConfigurationDto> Update([FromBody] NotificationConfigurationDto input)
        {
            return await _repository.UpdateNotificationConfiguration(input);
        }
        public override async Task<NotificationConfigurationDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        [HttpGet("getNotificationConfigurations")]
        public async Task<NotificationConfigurationDto> GetNotificationConfigurations()
        {
            return await _repository.GetNotificationConfigurations();
        }
        [HttpDelete("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }


    }
}
