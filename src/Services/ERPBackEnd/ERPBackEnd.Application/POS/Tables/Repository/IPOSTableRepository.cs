using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Repository
{
    public interface IPOSTableRepository
    {
        List<POSTableDto> GetTablesByFloorId(long FloorId);
        Task<ResponseResult<List<ReservedTablesDto>>> GetReservedTablesByFloorId(long floorId);



    }
}

