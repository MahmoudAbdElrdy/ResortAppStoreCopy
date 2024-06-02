using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Currencies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Repository
{
    public interface ICurrencyRepository
    {
        Task<CurrencyTransactionDto> CreateCurrencyTransactionCommand(CreateCurrencyTransactionCommand request);
        Task<CurrencyTransactionDto> DeleteCurrencyTransactionCommand(DeleteCurrencyTransactionCommand request);
        Task<int> DeleteListCurrencyTransactionCommand(DeleteListCurrencyTransactionCommand request);
        Task<CurrencyTransactionDto> EditCurrencyTransactionCommand(EditCurrencyTransactionCommand request);
        Task<CurrencyTransactionDto> GetByCurrencyTransactionId(GetByCurrencyTransactionId request);
        Task<List<CurrencyTransactionDto>> GetAllCurrenciesTransactionsQueries();
        Task<CurrencyDto> GetByCurrencyId(GetByCurrencyId request);
        Task<List<CurrencyDto>> GetAllCurrencies();

    }
}
