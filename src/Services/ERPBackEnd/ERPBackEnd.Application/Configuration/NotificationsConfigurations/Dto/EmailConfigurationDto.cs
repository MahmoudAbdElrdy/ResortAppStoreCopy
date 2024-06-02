using AutoMapper;
using Common.Mapper;
using Configuration.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto
{
    public class EmailConfigurationDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public long? NotificationConfigurationsId { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Smtp { get; set; }
        public string? Port { get; set; }
        public string? TypeCertificateId { get; set; }
        public bool? IsDefault { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EmailConfigurationDto, EmailConfiguration>()
                .ReverseMap();
            configuration.CreateMap<EmailConfiguration, EmailConfigurationDto>().ReverseMap();

        }

    }
}
