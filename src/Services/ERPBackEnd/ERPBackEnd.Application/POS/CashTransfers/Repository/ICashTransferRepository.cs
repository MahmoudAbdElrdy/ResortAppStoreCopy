using ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Dto;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Repository
{
    public interface ICashTransferRepository
    {

        Task<CashTransferDto> CreateCashTransfer(CashTransferDto input);
        Task<CashTransferDto> UpdateCashTransfer(CashTransferDto input);


    }
}

