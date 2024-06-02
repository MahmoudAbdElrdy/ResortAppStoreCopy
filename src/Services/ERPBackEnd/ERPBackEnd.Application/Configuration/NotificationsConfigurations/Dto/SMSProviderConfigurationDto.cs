using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto
{
    public class SMSProviderConfigurationDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public long NotificationConfigurationsId { get; set; }
        public string? SmsUrl { get; set; }
        public string? SmsUrlCredit { get; set; }
        public string? SmsUserName { get; set; }
        public string? Password { get; set; }
        public string? SmsSender { get; set; }
        public bool? IsDefault { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SMSProviderConfigurationDto, SMSProviderConfiguration>()
                .ReverseMap();
            configuration.CreateMap<SMSProviderConfiguration, SMSProviderConfigurationDto>().ReverseMap();

        }
    }
}
