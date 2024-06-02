using Common.SharedDto;
using Refit;
using System.Threading.Tasks;

namespace Nashmi.Services.NPay.Data.ExternalServices.PaymentApi
{
    public interface ISettingErpApi
    {


        [Post("/api/SettingErp/create-database")]
        Task<string> CreateDatabase(Common.SharedDto.SettingDataBaseDto databaseName);
        [Post("/api/SettingErp/login-owner")]
        Task<string> LoginOwner(AuthorizedUserOwnerDTO authorized);
       // Task<OutPutCreateDataBaseDto> CreateDataBase(InputCreateDataBaseDto request); 

       
    }
}
