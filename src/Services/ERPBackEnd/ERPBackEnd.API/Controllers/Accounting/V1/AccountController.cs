using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Dto;
using Common.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class AccountController : MainController<Account, AccountDto, long>
    {
        private IAccountRepository _accountRepository { get; set; }
        public AccountController(GMappRepository<Account, AccountDto, long> mainRepos, IAccountRepository accountRepository) : base(mainRepos)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<AccountTreeDto>>> ShowTree([FromQuery] GetAllAccountTreeCommand query)
        {
            return Ok(await _accountRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(string? parentId)
        {
            return Ok(await _accountRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<AccountDto>> Add([FromBody] AccountDto command)
        {

            return Ok(await _accountRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<AccountDto>> Edit([FromBody] AccountDto command)
        {

            return Ok(await _accountRepository.Update(command));
        }
        [HttpGet("deleteAccount")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<AccountDto>> Delete(string id)
        {
            return Ok(await _accountRepository.DeleteAccount(new DeleteAccountCommand() { Id = id }));
        }
        [HttpPost("isLeafAccount")]
        // [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<bool>> Edit([FromBody] DeleteAccountCommand id)
        {
            return Ok(await _accountRepository.IsLeafAccount(id));
        }
        [HttpGet("getLeafAccounts")]
        public async Task<ActionResult<IEnumerable<Account>>> getLeafAccounts(long AccountClassificationId)
        {
            return Ok(await _accountRepository.GetLeafAccounts(AccountClassificationId));
        }
        //[HttpGet("getAccountBalalnce")]
        //public async Task<List<AccountBalanceDto>> GetAccountBalalnce(string accountId)
        //{
        //    return await _accountRepository.GetAccountBalalnce(accountId);
        //}
        [HttpGet("getAccountBalalnce")]
        public IActionResult GetAccountBalalnce(string accountId)
        {
            var AccountBalalnce = _accountRepository.GetAccountBalalnce(accountId);
            return Ok(new { message = "AccountBalalnce:", data = AccountBalalnce });
        }


    }

}
