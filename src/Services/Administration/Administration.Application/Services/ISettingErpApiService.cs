using Nashmi.Services.NPay.Data.ExternalServices.PaymentApi;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Services;

public class ISettingErpApiService
{
    private readonly ISettingErpApi _api;
    //public async Task CallCreateDatabaseApi(string databaseName)
    //{
    //    try
    //    {
    //        var response = await _api.CreateDatabase(databaseName);
    //        Console.WriteLine("API call successful. Response: " + response);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Error calling API: " + ex.Message);
    //    }
    //}
}
