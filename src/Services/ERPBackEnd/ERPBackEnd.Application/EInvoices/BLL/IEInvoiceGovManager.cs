using Egypt_EInvoice_Api.EInvoiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.EInvoices.BLL
{
    public interface IEInvoiceGovManager
    {
        Task<LoginResponse> Login();
        void CreateESGCode(List<ESGItem> newItems);
    }
}
