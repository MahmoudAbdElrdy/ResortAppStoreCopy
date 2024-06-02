using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Repository
{
    public class PackageRepository : GMappRepository<Package, PackageDto, long>, IPackageRepository
    {
        private readonly IGRepository<Package> _context;
        private readonly IGRepository<PackagesModules> _packageModulesContext;
        private readonly IGRepository<Module> _modulesContext;
        private readonly IMapper _mapper;

        public PackageRepository(IGRepository<Package> mainRepos,
            IGRepository<PackagesModules> packageModulesContext,
              IGRepository<Module> modulesContext,
            IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
            _packageModulesContext = packageModulesContext;
            _modulesContext = modulesContext;
        }
        public async Task<PackageDto> CreatePackageCommand(PackageDto request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var package = _mapper.Map<Package>(request);
            package.CreatedAt = DateTime.Now;
            package.CreatedBy = "admin";
            package.IsDeleted = false;
            package.IsActive = true;
            var newPackage = await _context.InsertAsync(package);
            var result = await _context.SaveChangesAsync();
            if (result > 0 && newPackage.Id > 0)
            {
                
                    foreach (var item in request.ModuleIds)
                    {
                        await _packageModulesContext.InsertAsync(new PackagesModules
                        {
                            Id = 0,
                            ModuleId = item,
                            PackageId = newPackage.Id
                        });

                    }
                    await _packageModulesContext.SaveChangesAsync();
           
           
            }


            return _mapper.Map<PackageDto>(newPackage);

        }

        public async Task<List<PackageListDto>> GetAll()
        {
            List<PackageListDto> list = new List<PackageListDto>();
            var packageList = await _context.GetAll().Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
            list = _mapper.Map<List<PackageListDto>>(packageList);
            var packageIdList = packageList.Select(x => x.Id);
            var pakageModuleList = await _packageModulesContext.GetAll().Where(x => packageIdList.Contains(x.PackageId)).ToListAsync();
            var moduleList = await _modulesContext.GetAll().Where(x => pakageModuleList.Select(r => r.ModuleId).Contains(x.Id)).ToListAsync();
            
            list.ForEach(r =>
            {
                var pakageModules = pakageModuleList.Where(x => x.PackageId == r.Id);
                if (pakageModules.Count() > default(int))
                {
                    var moduleIdList = pakageModules.Select(y => y.ModuleId);
                    r.Modules.AddRange(_mapper.Map<List<ModuleDto>>(moduleList.Where(y => moduleIdList.Contains(y.Id))));
                }

            });

            return list;


        }

        public async Task<PackageDto> GetById(long id)
        {
            PackageDto packageDto = new PackageDto();
            if (id != default)
            {
                var package = await _context.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
                if (package != null)
                {
                    packageDto = _mapper.Map<PackageDto>(package);
                    packageDto.ModuleIds = await _packageModulesContext.GetAll()
                                            .Where(x => x.PackageId.Equals(id)).Select(x => x.ModuleId).ToListAsync();
                }
            }

            return packageDto;
        }
    }
}
