
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class POSBillTypeController : MainController<POSBillType, POSBillTypeDto, long>
    {
        private readonly GMappRepository<POSBillType, POSBillTypeDto, long> _mainRepos;
        private readonly IPOSBillTypeRepository _billTypeRepos;
        private readonly IUserRepository _userRepos;
  
        public POSBillTypeController(GMappRepository<POSBillType, POSBillTypeDto, long> mainRepos, IPOSBillTypeRepository billTypeRepos, IUserRepository userRepos) : base(mainRepos)
        {
            
            mainRepos.PermissionSpName = "SP_Create_POS_Bill_Permission";
            _mainRepos = mainRepos;
            _userRepos = userRepos;
            _billTypeRepos = billTypeRepos;

        }


        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<POSBillTypeDto> Create([FromBody] POSBillTypeDto input) { 

            return await _mainRepos.CreateWithPermission(input, GetCurrentUserRoleId());

        }
        [HttpPost("editWithPerssion")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<POSBillTypeDto> Update([FromBody] POSBillTypeDto input)
        {
            return await _billTypeRepos.UpdateBillType(input);
        }
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override  Task Delete(long id)
        {
            
            return _billTypeRepos.DeleteAsync(id);
        }
        public override async Task<POSBillTypeDto> GetById(long id)
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
