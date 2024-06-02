using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto
{
    public class NotificationConfigurationDto:IHaveCustomMapping
    {
        public NotificationConfigurationDto()
        {
            EmailConfigurations = new List<EmailConfigurationDto>();
            SMSProviderConfigurations = new List<SMSProviderConfigurationDto>();
            WhatsAppConfigurations = new List<WhatsAppCongigurationDto>();


        }
        public long Id { get; set; }
        public virtual List<EmailConfigurationDto>EmailConfigurations { get; set; }
        public virtual List<SMSProviderConfigurationDto> SMSProviderConfigurations { get; set; }
        public virtual List<WhatsAppCongigurationDto> WhatsAppConfigurations { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<NotificationConfigurationDto, NotificationConfiguration>()
                .ReverseMap();
            configuration.CreateMap<NotificationConfiguration, NotificationConfigurationDto>().ReverseMap();

        }
    }
}
