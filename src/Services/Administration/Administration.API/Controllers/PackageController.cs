using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Repository;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Repository;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : MainController<Package, PackageDto, long>
    {
        private IPackageRepository _repositoryPackage;
        public PackageController(GMappRepository<Package, PackageDto, long> mainRepos,
            IPackageRepository repositoryPackage) : base(mainRepos)
        {
            _repositoryPackage = repositoryPackage;
        }


        [HttpPost("Create")]
        [SuccessResultMessage("addSuccessfully")]
        public async Task<ActionResult<PackageDto>> Create([FromBody] PackageDto input)
        {
            return Ok(await _repositoryPackage.CreatePackageCommand(input));
        }


        [HttpGet("GetAllPackages")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PackageListDto>>> GetAll()
        {
            return Ok(await _repositoryPackage.GetAll());
        }

        [HttpGet("GetPackagebyId/{id}")]
       
        public async Task<ActionResult<PackageDto>> GetPackagebyId([FromRoute] long id)
        {
            return Ok(await _repositoryPackage.GetById(id));
        }

    }
}
