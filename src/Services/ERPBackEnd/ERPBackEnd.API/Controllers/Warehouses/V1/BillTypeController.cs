
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class BillTypeController : MainController<BillType, BillTypeDto, long>
    {
        private readonly GMappRepository<BillType, BillTypeDto, long> _mainRepos;
        private readonly IBillTypeRepository _billTypeRepos;
        private readonly IUserRepository _userRepos;
  
        public BillTypeController(GMappRepository<BillType, BillTypeDto, long> mainRepos, IBillTypeRepository billTypeRepos, IUserRepository userRepos) : base(mainRepos)
        {
            
            mainRepos.PermissionSpName = "SP_Create_Bill_Permission";
            _mainRepos = mainRepos;
            _userRepos = userRepos;
            _billTypeRepos = billTypeRepos;

        }


        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<BillTypeDto> Create([FromBody] BillTypeDto input) { 

            return await _mainRepos.CreateWithPermission(input, GetCurrentUserRoleId());

        }
        [HttpPost("editWithPerssion")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<BillTypeDto> Update([FromBody] BillTypeDto input)
        {
            return await _billTypeRepos.UpdateBillType(input);
        }
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override  Task Delete(long id)
        {
            
            return _billTypeRepos.DeleteAsync(id);
        }
        public override async Task<BillTypeDto> GetById(long id)
        {
            return await _billTypeRepos.FirstInclude(id);
        }

        protected string GetCurrentUserRoleId()
        {
            var userData = HttpContext.User.Claims.Where(x => x.Type == "userLoginId").First();
            return _userRepos.GetRoleByUserId(userData.Value);
          
        }





    }
}
