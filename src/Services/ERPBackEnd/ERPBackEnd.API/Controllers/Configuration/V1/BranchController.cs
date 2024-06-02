using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ERPBackEnd.API.Helpers;
using Configuration.Entities;
using Common.Repositories;
using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Branches.Repository;
using Common.Exceptions;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class BranchController : MainController<Branch, BranchDto, long>
    {
        IBranchRepository _branchRepository { get; set; } 
        public BranchController(GMappRepository<Branch, BranchDto, long> mainRepos, IBranchRepository branchRepository) : base(mainRepos)
        {
            _branchRepository = branchRepository;
        }
        public override async Task<ActionResult<PageList<BranchDto>>> GetAll([FromQuery] Paging paging)
        {
            return Ok(await _branchRepository.GetAllIncluding(paging));
        }
        //public override async Task<BranchDto> Create([FromBody] BranchDto input)
        //{
        //    return await _branchRepository.Add(input);
        //}
        [HttpPost("AllBranchPermission")]
      
        public async Task<List<BranchPermissionListDto>> GetAll([FromBody] InputBranchPermissionDto branchIds) 
        {
            return await _branchRepository.AllBranchPermissionList(branchIds.branchIds,branchIds.userId);
        }
        [HttpPost("EditBranchPermission")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<bool> Create(List<BranchPermissionDto> branchPermissions) 
        {
            return await _branchRepository.EditBranchPermission(branchPermissions);
        }
    }
  
}
