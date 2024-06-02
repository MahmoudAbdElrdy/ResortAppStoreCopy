using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountGroups.Dto;
using Common.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountGroups.Repository;
using Common.Infrastructures;
using Common.Exceptions;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class AccountGroupController : MainController<AccountGroup, AccountGroupDto, long>
    {
        private IAccountGroupRepository _accountGroupRepository { get; set; } 
        public AccountGroupController(GMappRepository<AccountGroup, AccountGroupDto, long> mainRepos, IAccountGroupRepository accountGroupRepository) : base(mainRepos)
        {
            _accountGroupRepository = accountGroupRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<TreeDto>>> ShowTree([FromQuery] GetAllAccountGroupTreeCommand query)
        {
            return Ok(await _accountGroupRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(long? parentId)
        {
            return Ok(await _accountGroupRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<AccountGroupDto>> Add([FromBody] AccountGroupDto command)
        {
            return Ok(await _accountGroupRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<AccountGroupDto>> Edit([FromBody] AccountGroupDto command)
        {

            return Ok(await _accountGroupRepository.Update(command));
        }
        [HttpGet("deleteAccountGroup")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<AccountGroupDto>> Delete(long id)  
        {
            return Ok(await _accountGroupRepository.DeleteAccountGroup(new DeleteAccountGroupCommand() { Id = id }));
        }
    }
}