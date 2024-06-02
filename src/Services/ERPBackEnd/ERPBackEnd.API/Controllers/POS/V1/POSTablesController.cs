using Common.BaseController;
using Common.Constants;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class POSTablesController : MainController<POSTable, POSTableDto, long>
    {
        private IPOSTableRepository _repository;

        public POSTablesController(GMappRepository<POSTable, POSTableDto, long> mainRepos, IPOSTableRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        [HttpGet("getTablesByFloorId")]

        public List<POSTableDto> GetTablesByFloorId(long FloorId)
        {
            return _repository.GetTablesByFloorId(FloorId);
        }
        [HttpGet("getReservedTablesByFloorId")]

        public async Task<ResponseResult<List<ReservedTablesDto>>> GetReservedTablesByFloorId(long FloorId)
        {
            return await _repository.GetReservedTablesByFloorId(FloorId);
        }
    }
}
