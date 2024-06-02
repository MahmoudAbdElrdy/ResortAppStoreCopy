using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Dto;
using Configuration.Entities;
using Configuration.Repository;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Repository
{
    public class NotificationConfigurationRepository : GMappRepository<NotificationConfiguration, NotificationConfigurationDto, long>, INotificationConfigurationRepository
    {
        private readonly IGRepository<NotificationConfiguration> _notificationConfigurationRepos;
        private IGRepository<EmailConfiguration> _emailConfigurationRepos { get; set; }
        private IGRepository<WhatsAppConfiguration> _whatsAppCongigurationRepos { get; set; }

        private IGRepository<SMSProviderConfiguration> _sMSProviderConfigurationRepos { get; set; }

        private IMapper _mpper;


        public NotificationConfigurationRepository(IGRepository<NotificationConfiguration> mainRepos, IMapper mapper, DeleteService deleteService
            , IGRepository<EmailConfiguration> emailConfigurationRepos,
            IGRepository<WhatsAppConfiguration> whatsAppCongigurationRepos,
            IGRepository<SMSProviderConfiguration> sMSProviderConfigurationRepos
            ) : base(mainRepos, mapper, deleteService)
        {
            _notificationConfigurationRepos = mainRepos;
            _sMSProviderConfigurationRepos = sMSProviderConfigurationRepos;
            _whatsAppCongigurationRepos = whatsAppCongigurationRepos;
            _mpper = mapper;
            _emailConfigurationRepos = emailConfigurationRepos;

        }
        public async Task<NotificationConfigurationDto> FirstInclude(long id)
        {
            var item = await _notificationConfigurationRepos.GetAllIncluding(c => c.EmailConfigurations).Include(c=>c.SMSProviderConfigurations).Include(c=>c.WhatsAppConfigurations).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mpper.Map<NotificationConfigurationDto>(item);
            return result;
        }
        public async Task<NotificationConfigurationDto> GetNotificationConfigurations()
        {
            var item = await  _notificationConfigurationRepos.GetAllIncluding(c => c.EmailConfigurations).Include(c => c.SMSProviderConfigurations).Include(c => c.WhatsAppConfigurations).AsNoTracking().FirstOrDefaultAsync();

            var result = _mpper.Map<NotificationConfigurationDto>(item);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {

            var notificationConfigurationResult = await FirstInclude(id);

            if (notificationConfigurationResult?.EmailConfigurations != null)
            {
                foreach (var item in notificationConfigurationResult?.EmailConfigurations)
                {
                    await _emailConfigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (notificationConfigurationResult?.WhatsAppConfigurations != null)
            {
                foreach (var item in notificationConfigurationResult?.WhatsAppConfigurations)
                {
                    await _whatsAppCongigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (notificationConfigurationResult?.SMSProviderConfigurations != null)
            {
                foreach (var item in notificationConfigurationResult?.SMSProviderConfigurations)
                {
                    await _sMSProviderConfigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "EmailConfiguration", "WhatsAppCongiguration", "SMSProviderConfiguration" }, "NotificationConfiguration", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var notificationConfigurationResult = await FirstInclude(Convert.ToInt64(id));

                if (notificationConfigurationResult?.EmailConfigurations != null)
                {
                    foreach (var item in notificationConfigurationResult?.EmailConfigurations)
                    {
                        await _emailConfigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (notificationConfigurationResult?.WhatsAppConfigurations != null)
                {
                    foreach (var item in notificationConfigurationResult?.WhatsAppConfigurations)
                    {
                        await _whatsAppCongigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (notificationConfigurationResult?.SMSProviderConfigurations != null)
                {
                    foreach (var item in notificationConfigurationResult?.SMSProviderConfigurations)
                    {
                        await _sMSProviderConfigurationRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "EmailConfiguration", "WhatsAppCongiguration", "SMSProviderConfiguration" }, "NotificationConfiguration", "Id");
        }
        public async Task<NotificationConfigurationDto> CreateNotificationConfiguration(NotificationConfigurationDto input)
        {
            var result = await base.Create(input);
            return input;
        }


        public async Task<NotificationConfigurationDto> UpdateNotificationConfiguration(NotificationConfigurationDto input)
        {
            var items = await _notificationConfigurationRepos.GetAllIncluding(c => c.EmailConfigurations).AsNoTracking().FirstOrDefaultAsync(c => c.Id == input.Id);
            var notificatonConfigResult = _mpper.Map<NotificationConfigurationDto>(items);
            if (notificatonConfigResult?.EmailConfigurations != null)
            {
                foreach (var item in notificatonConfigResult?.EmailConfigurations)
                {
                    var entity = _mpper.Map<EmailConfiguration>(item);
                    await _emailConfigurationRepos.SoftDeleteAsync(entity);
                }

            }
            if (notificatonConfigResult?.EmailConfigurations != null)
            {
                foreach (var item in notificatonConfigResult?.WhatsAppConfigurations)
                {
                    var entity = _mpper.Map<WhatsAppConfiguration>(item);
                    await _whatsAppCongigurationRepos.SoftDeleteAsync(entity);
                }

            }
            if (notificatonConfigResult?.SMSProviderConfigurations != null)
            {
                foreach (var item in notificatonConfigResult?.SMSProviderConfigurations)
                {
                        var entity = _mpper.Map<SMSProviderConfiguration>(item);
                        await _sMSProviderConfigurationRepos.SoftDeleteAsync(entity);
                    }

                }

            var result = await base.Update(input);
            return input;
        }
        
        
    }
}