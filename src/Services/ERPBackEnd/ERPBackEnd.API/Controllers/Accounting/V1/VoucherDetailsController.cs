using AutoMapper;
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class VoucherDetailsController : MainController<VoucherDetail, VoucherDetailDto, long>
    {
        private IVoucherDetailsRepository _repository;
        public VoucherDetailsController(GMappRepository<VoucherDetail, VoucherDetailDto, long> mainRepos, IMapper mapper, IVoucherDetailsRepository repository) : base(mainRepos)
        {
            _repository = repository;
        } 
        public override Task Delete(long id)
        {
            return _repository.DeleteAsync(id);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }

    }
}
