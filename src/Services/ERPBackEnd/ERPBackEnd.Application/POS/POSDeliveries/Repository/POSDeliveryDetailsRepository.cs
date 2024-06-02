using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Repository
{
    public class POSDeliveryDetailsRepository : GMappRepository<POSDeliveryDetail, POSDeliveryDetailsDto, long>, IPOSDeliveryDetailsRepository
    {

        public POSDeliveryDetailsRepository(IGRepository<POSDeliveryDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
          


        }


        public async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "POSDeliveryDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "POSDeliveryDetails", "Id");
        }
    }
}
