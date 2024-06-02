using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ERPBackEnd.API.Helpers;
using Common.Repositories;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CompanyController : MainController<Company, CompanyDto, long>
    {
        private ICompanyRepository _companyRepository { get; set; } 
        public CompanyController(GMappRepository<Company, CompanyDto, long> mainRepos, ICompanyRepository companyRepository) : base(mainRepos)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        [Route("get-all")]
        public override async Task<ActionResult<PageList<CompanyDto>>> GetAll([FromQuery] Paging paging)
        {
            return Ok(await _companyRepository.GetAllIncluding(paging));
        }
    }
  
}
