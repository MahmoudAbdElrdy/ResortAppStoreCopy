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
    public class WhatsAppCongigurationDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long? NotificationConfigurationsId { get; set; }
        public string? WhatsAppProvider { get; set; }
        public string? WhatsAppAccount { get; set; }
        public string? WhatsAppAccountSid { get; set; }
        public bool? isDefault { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<WhatsAppCongigurationDto, WhatsAppConfiguration>()
                .ReverseMap();
            configuration.CreateMap<WhatsAppConfiguration, WhatsAppCongigurationDto>().ReverseMap();

        }
    }
}
