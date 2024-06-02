using AutoMapper;
using Common.Extensions;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription
{
    public class ModuleRepo : GMappRepository<Module, ModuleDto, long>, IModuleRepo
    {
        private readonly IGRepository<Module> _context;
        private readonly IMapper _mapper;

        public ModuleRepo(IGRepository<Module> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
        }

        public async Task<ModuleDto> EditModule(EditModuleCommand request)
        {
            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (entityDb == null)
                throw new UserFriendlyException("Not Found");

            entityDb = _mapper.Map(request.InputDto, entityDb);
            await _context.UpdateAsync(entityDb);
            await _context.SaveChangesAsync();
            return _mapper.Map<ModuleDto>(entityDb);
        }

        public async Task<List<ModuleDto>> GetAllModule()
        {
            var moduleList = await _context.GetAll().Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<ModuleDto>>(moduleList);

            return res;
        }

        public async Task<ModuleDto> GetModuleById(long id)
        {
            var module = await _context.FirstOrDefaultAsync(x =>  x.Id == id);

            if (module != null)
            {
                var res = _mapper.Map<ModuleDto>(module);
                return res;
            }

            return new ModuleDto();
        }
    }
}
