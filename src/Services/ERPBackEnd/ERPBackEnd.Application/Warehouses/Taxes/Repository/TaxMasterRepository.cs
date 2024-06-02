using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository
{
    public class TaxMasterRepository : GMappRepository<TaxMaster,TaxMasterDto, long>, ITaxMasterRepository
    {
        private readonly IGRepository<TaxMaster> _TaxMasterRepos;
        private IGRepository<TaxDetail> _TaxDetailRepos { get; set; }
        private IGRepository<SubTaxDetail> _SubTaxDetail { get; set; }
        private IGRepository<SubTaxReasonsDetail> _SubTaxReasonsDetailRepos { get; set; }
        private IGRepository<SubTaxRatioDetail> _SubTaxRatioDetailRepos { get; set; }


        private IMapper _mpper;


        public TaxMasterRepository(IGRepository<TaxMaster> mainRepos, IMapper mapper, DeleteService deleteService
            ,IGRepository<TaxDetail> TaxDetailRepos, IGRepository<SubTaxDetail> SubTaxDetail , IGRepository<SubTaxReasonsDetail> SubTaxReasonsDetailRepos, IGRepository<SubTaxRatioDetail> SubTaxRatioDetailRepos
            ) : base(mainRepos, mapper, deleteService)
        {
            _TaxMasterRepos = mainRepos;
            _mpper = mapper;
            _TaxDetailRepos = TaxDetailRepos;
            _SubTaxDetail = SubTaxDetail;
            _SubTaxRatioDetailRepos = SubTaxRatioDetailRepos;   
            _SubTaxReasonsDetailRepos = SubTaxReasonsDetailRepos;


        }
        public async Task<TaxMasterDto> FirstInclude(long id)
        {
            var item = await _TaxMasterRepos.GetAllIncluding(c => c.TaxDetail).Include(c => c.SubTaxDetail).ThenInclude(s => s.SubTaxReasonsDetail).Include(s=>s.SubTaxDetail).ThenInclude(s=>s.SubTaxRatioDetail).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            var result = _mpper.Map<TaxMasterDto>(item);
            return result;
        }

        public  async Task<int> DeleteAsync(long id)
        {
            
            var TaxResult = await FirstInclude(id);

            if (TaxResult?.TaxDetail != null)
            {
                foreach (var item in TaxResult?.TaxDetail)
                {
                    await _TaxDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    await _SubTaxDetail.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    foreach (var subItem in item.SubTaxReasonsDetail)
                    {
                        await _SubTaxReasonsDetailRepos.SoftDeleteWithoutSaveAsync(subItem.Id);
                    }

                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    foreach (var subItem in item.SubTaxRatioDetail)
                    {
                        await _SubTaxRatioDetailRepos.SoftDeleteWithoutSaveAsync(subItem.Id);
                    }

                }

            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "TaxDetails", "SubTaxDetails" }, "TaxMasters", "Id");

           
        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
               
                var TaxResult = await FirstInclude(Convert.ToInt64(id));

                if (TaxResult?.TaxDetail != null)
                {
                    foreach (var item in TaxResult?.TaxDetail)
                    {
                        await _TaxDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (TaxResult?.SubTaxDetail != null)
                {
                    foreach (var item in TaxResult?.SubTaxDetail)
                    {
                        await _SubTaxDetail.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (TaxResult?.SubTaxDetail != null)
                {
                    foreach (var item in TaxResult?.SubTaxDetail)
                    {
                        foreach (var subItem in item.SubTaxReasonsDetail)
                        {
                            await _SubTaxReasonsDetailRepos.SoftDeleteWithoutSaveAsync(subItem.Id);
                        }
                      
                    }

                }
                if (TaxResult?.SubTaxDetail != null)
                {
                    foreach (var item in TaxResult?.SubTaxDetail)
                    {
                        foreach (var subItem in item.SubTaxRatioDetail)
                        {
                            await _SubTaxRatioDetailRepos.SoftDeleteWithoutSaveAsync(subItem.Id);
                        }

                    }

                }

            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "TaxDetails", "SubTaxDetails" }, "TaxMasters", "Id");
        }
        public async Task<TaxMasterDto> CreateTax(TaxMasterDto input)
        {
            var result = await base.Create(input);
            return result;
        }
    
        public async Task<TaxMasterDto> UpdateTax(TaxMasterDto input)
        {
            var TaxItem = await _TaxMasterRepos.GetAllIncluding(c => c.TaxDetail).Include(c => c.SubTaxDetail).ThenInclude(s => s.SubTaxReasonsDetail).Include(s => s.SubTaxDetail).ThenInclude(s => s.SubTaxRatioDetail).AsNoTracking().FirstOrDefaultAsync(c => c.Id == input.Id);
            var TaxResult = _mpper.Map<TaxMasterDto>(TaxItem);
            if (TaxResult?.TaxDetail != null)
            {
                foreach (var item in TaxResult?.TaxDetail)
                {
                    var entity = _mpper.Map<TaxDetail>(item);
                    await _TaxDetailRepos.SoftDeleteAsync(entity);
                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    var entity = _mpper.Map<SubTaxDetail>(item);
                    await _SubTaxDetail.SoftDeleteAsync(entity);
                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    foreach (var subItem in item.SubTaxReasonsDetail)
                    {
                        var entity = _mpper.Map<SubTaxReasonsDetail>(subItem);
                        await _SubTaxReasonsDetailRepos.SoftDeleteAsync(entity);
                    }
                }

            }
            if (TaxResult?.SubTaxDetail != null)
            {
                foreach (var item in TaxResult?.SubTaxDetail)
                {
                    foreach (var subItem in item.SubTaxRatioDetail)
                    {
                        var entity = _mpper.Map<SubTaxRatioDetail>(subItem);
                        await _SubTaxRatioDetailRepos.SoftDeleteAsync(entity);
                    }
                }

            }

            var result = await base.Update(input);
            return input;

        }
        

    }
}
