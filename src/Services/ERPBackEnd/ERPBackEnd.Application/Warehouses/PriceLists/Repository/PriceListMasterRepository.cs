using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Repository
{
    public class PriceListMasterRepository : GMappRepository<PriceListMaster, PriceListMasterDto, long>, IPriceListMasterRepository
    {
        private readonly IGRepository<PriceListMaster> _priceListMasterRepos;
        private IGRepository<PriceListDetail> _priceListDetailRepos { get; set; }
        private IMapper _mpper;


        public PriceListMasterRepository(IGRepository<PriceListMaster> mainRepos, IMapper mapper, DeleteService deleteService
            ,IGRepository<PriceListDetail> priceListDetailRepos
            ) : base(mainRepos, mapper, deleteService)
        {
            _priceListMasterRepos = mainRepos;
            _mpper = mapper;
            _priceListDetailRepos = priceListDetailRepos;
         


        }
        public async Task<PriceListMasterDto> FirstInclude(long id)
        {
            var item = await _priceListMasterRepos.GetAllIncluding(c => c.PriceListDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            var result = _mpper.Map<PriceListMasterDto>(item);
            return result;
        }

        public  async Task<int> DeleteAsync(long id)
        {
            
            var priceListResult = await FirstInclude(id);

            if (priceListResult?.PriceListDetails != null)
            {
                foreach (var item in priceListResult?.PriceListDetails)
                {
                    await _priceListDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
          
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "PriceListDetails" }, "PriceListMasters", "Id");

           
        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
               
                var priceListResult = await FirstInclude(Convert.ToInt64(id));

                if (priceListResult?.PriceListDetails != null)
                {
                    foreach (var item in priceListResult?.PriceListDetails)
                    {
                        await _priceListDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
               
              
            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "PriceListDetails" }, "PriceListMasters", "Id");
        }
        public async Task<PriceListMasterDto> CreatePriceList(PriceListMasterDto input)
        {
            var result = await base.Create(input);
            return result;
        }
    
        public async Task<PriceListMasterDto> UpdatePriceList(PriceListMasterDto input)
        {
            var priceListResult = await FirstInclude(input.Id);

            
            if (priceListResult?.PriceListDetails != null)
            {
                foreach (var item in priceListResult?.PriceListDetails)
                {
                    var entity = _mpper.Map<PriceListDetail>(item);
                    await _priceListDetailRepos.SoftDeleteAsync(entity);
                }

            }
          

            var result = await base.Update(input);
            return input;

        }
        

    }
}
