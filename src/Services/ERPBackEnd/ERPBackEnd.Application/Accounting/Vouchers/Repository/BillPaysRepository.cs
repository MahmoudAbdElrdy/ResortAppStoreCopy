using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository
{
    public class BillPaysRepository : GMappRepository<BillPay, BillPayDto, long>, IBillPaysRepository
    {
        public BillPaysRepository(IGRepository<BillPay> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }

       
       
    }
}
