using AutoMapper;
using Common.Extensions;
using Common.Infrastructures;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Repository
{
    public class GeneralConfigurationsRepository : GMappRepository<GeneralConfiguration, GeneralConfigurationDto, long>, IGeneralConfigurationsRepository
    {
        private readonly IGRepository<GeneralConfiguration> _context;
        private readonly IMapper _mapper;

        public GeneralConfigurationsRepository(IGRepository<GeneralConfiguration> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GeneralConfigurationDto>> GetAllGeneralConfigurationWithPaginatio(GetAllGeneralConfigurationWithPagination request)
        {
            var query = _context.GetAllIncluding().
            Where(c => !c.IsDeleted)
                  .WhereIf(request.ModuleType != null && request.ModuleType > 0, c => c.ModuleType.Equals(request.ModuleType));

            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.NameAr.Contains(request.Filter));
            }

            var entities = query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize).ToList();

            var totalCount = await query.CountAsync();

            var transferReasonDto = _mapper.Map<List<GeneralConfigurationDto>>(entities);

            return new PaginatedList<GeneralConfigurationDto>(transferReasonDto,
                totalCount,
                request.PageIndex,
                request.PageSize);

        }
        public async Task<GeneralConfigurationDto> EditGeneralConfiguration(Dto.EditGeneralConfigurationCommand request)
        {
            foreach (var item in request.generalConfiguration)
            {
                var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == item.Id);
                if (entityDb == null)
                {
                    // throw new UserFriendlyException("Not Found");
                    break;
                }

                entityDb = _mapper.Map(item, entityDb);
                await _context.UpdateAsync(entityDb);
            }

            await _context.SaveChangesAsync();



            return _mapper.Map<GeneralConfigurationDto>(request?.generalConfiguration?.FirstOrDefault());
        }
        public async Task<GeneralConfigurationDto> GetById(int Id)
        {
            var generalConfiguration = await _context.GetAll().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);
            var generalConfigurationDto = _mapper.Map<GeneralConfigurationDto>(generalConfiguration);

         
            return generalConfigurationDto;

        }
        public async Task<GeneralConfigurationDto> GetDdl()
        {
            var generalConfiguration = await _context.GetAll().FirstOrDefaultAsync(x=>!x.IsDeleted);
            var generalConfigurationDto = _mapper.Map<GeneralConfigurationDto>(generalConfiguration);


            return generalConfigurationDto;

        }

    }
}
