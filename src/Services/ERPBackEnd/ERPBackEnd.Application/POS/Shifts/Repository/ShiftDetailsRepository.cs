using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Repository
{
    public class ShiftDetailsRepository : GMappRepository<ShiftDetail, ShiftDetailsDto, long>, IShiftDetailsRepository
    {




        public ShiftDetailsRepository(IGRepository<ShiftDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
          


        }


        public async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "ShiftDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "ShiftDetails", "Id");
        }
    }
}
