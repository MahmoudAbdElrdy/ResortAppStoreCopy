using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Repository
{
    public interface IPackageRepository
    {
        Task<PackageDto> CreatePackageCommand(PackageDto request);

        Task<List<PackageListDto>> GetAll();
        Task<PackageDto> GetById(long id);

    }
}
