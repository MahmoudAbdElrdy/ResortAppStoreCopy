using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Journals.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class PointOfSaleCardController : MainController<PointOfSaleCard, PointOfSaleCardDto, long>
    {

        public PointOfSaleCardController(GMappRepository<PointOfSaleCard, PointOfSaleCardDto, long> mainRepos) : base(mainRepos)
        {

        }

    }
}
