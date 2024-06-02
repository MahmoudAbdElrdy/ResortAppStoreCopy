using AutoMapper;
using Common.Extensions;
using Common.Infrastructures;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Settings.GeneralConfigurations.Dto;
using ResortAppStore.Services.Administration.Application.Settings.Settings.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
namespace ResortAppStore.Services.Administration.Application.Settings.Settings.Repository
{
    public class SettingsRepository : GMappRepository<Setting, SettingDto, long>, ISettingsRepository
    {
        private readonly IGRepository<Setting> _context;
        private readonly IMapper _mapper;

        public SettingsRepository(IGRepository<Setting> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SettingDto>> GetAllSetting()
        {
            var query = _context.GetAllIncluding().
            Where(c => !c.IsDeleted);

            var transferReasonDto = _mapper.Map<List<SettingDto>>(query);

            return new PaginatedList<SettingDto>(transferReasonDto,
                100,
                1,
                50);

        }
        public async Task<SettingDto> EditSetting(EditSettingDto request)
        {
            foreach (var item in request.Setting)
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



            return _mapper.Map<SettingDto>(request?.Setting?.FirstOrDefault());
        }
        public async Task<SettingDto> GetById(int Id)
        {
            var Setting = await _context.GetAll().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == Id);
            var SettingDto = _mapper.Map<SettingDto>(Setting);

         
            return SettingDto;

        }
      
    }
}
