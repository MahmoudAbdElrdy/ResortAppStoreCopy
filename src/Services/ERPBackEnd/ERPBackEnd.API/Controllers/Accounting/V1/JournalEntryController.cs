using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class JournalEntryController : MainController<JournalEntriesMaster, JournalEntriesMasterDto, long>
    {
        private IJournalEntriesRepository _repository;
        public JournalEntryController(
            GMappRepository<JournalEntriesMaster, JournalEntriesMasterDto, long> mainRepos, IMapper mapper,
            IJournalEntriesRepository repository) : base(mainRepos)
        {
            _repository = repository;
        }
        public override async Task<ActionResult<PageList<JournalEntriesMasterDto>>> GetAll([FromQuery] Paging paging)
        {
            return  Ok(await _repository.GetAllList(paging));
        }
        public override async Task<JournalEntriesMasterDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        public override Task Delete(long id)
        {
            return _repository.DeleteAsync(id);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }
        [HttpPost("Post")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<int>> Post([FromBody] List<long> ids)
        {
            return await _repository.UpdateListAsync(ids);
        }
        #region getJournalEntries 
        [HttpGet("getJournalEntries")]
        public IActionResult Show()
        {
            var JournalEntries = _repository.GetJournalEntries();
            return Ok(new { message = "JournalEntries:", data = JournalEntries });
        }

        #endregion

        [HttpGet("getJournalEntryAdditionalById")]
        public IActionResult GetJournalEntryAdditionalById(long Id)
        {
            var JournalEntries = _repository.GetJournalEntryById(Id);
            return Ok(new { message = "JournalEntry:", data = JournalEntries });
        }

    }
}
