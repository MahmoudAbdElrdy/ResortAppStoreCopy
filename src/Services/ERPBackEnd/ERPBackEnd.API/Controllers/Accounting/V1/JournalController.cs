using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Journals.Dto;
using Common.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class JournalController : MainController<Journal, JournalDto, long>
    {

        public JournalController(GMappRepository<Journal, JournalDto, long> mainRepos) : base(mainRepos)
        {

        }

    }
}