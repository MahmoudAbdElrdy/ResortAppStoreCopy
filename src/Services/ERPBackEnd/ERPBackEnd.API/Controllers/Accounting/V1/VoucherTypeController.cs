using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class VoucherTypeController : MainController<VoucherType, VoucherTypeDto, long>
    {
        private IVoucherTypeRepository _repository;
        private readonly GMappRepository<VoucherType, VoucherTypeDto, long> _mainRepos;
        private readonly IUserRepository _userRepos;
        public VoucherTypeController(GMappRepository<VoucherType, VoucherTypeDto, long> mainRepos, IMapper mapper, IVoucherTypeRepository repository, IUserRepository userRepos) : base(mainRepos)
        {
            mainRepos.PermissionSpName = "SP_Create_Voucher_Permission";
            _repository = repository;
            _userRepos = userRepos;
            _mainRepos = mainRepos;
        }
        public override Task Delete(long id)
        {
            return _repository.DeleteAsync(id);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }



        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<VoucherTypeDto> Create([FromBody] VoucherTypeDto input)
        {

            return await _mainRepos.CreateWithPermission(input, GetCurrentUserRoleId());

        }


        protected string GetCurrentUserRoleId()
        {
            var userData = HttpContext.User.Claims.Where(x => x.Type == "userLoginId").First();
            return _userRepos.GetRoleByUserId(userData.Value);

        }


    }
}
