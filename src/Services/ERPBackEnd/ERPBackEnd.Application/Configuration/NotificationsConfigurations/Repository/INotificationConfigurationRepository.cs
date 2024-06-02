using Configuration.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.NotificationsConfigurations.Repository
{
    public interface  INotificationConfigurationRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<NotificationConfigurationDto> CreateNotificationConfiguration(NotificationConfigurationDto input);
        Task<NotificationConfigurationDto> UpdateNotificationConfiguration(NotificationConfigurationDto input);
        Task<NotificationConfigurationDto> FirstInclude(long id);
        Task<NotificationConfigurationDto> GetNotificationConfigurations();
    }
}
