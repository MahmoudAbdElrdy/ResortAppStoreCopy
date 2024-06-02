using AutoMapper;
using Common.Constants;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Repository
{
    public class POSTableRepository : GMappRepository<POSTable, POSTableDto, long>, IPOSTableRepository
    {
        private readonly IGRepository<POSTable> _posTableRepos;
        private IMapper _mpper;
        private IAuditService _auditService;


        public POSTableRepository(IGRepository<POSTable> mainRepos, IMapper mapper, DeleteService deleteService,
             IAuditService auditService

            ) : base(mainRepos, mapper, deleteService)
        {
            _posTableRepos = mainRepos;
            _mpper = mapper;
            _auditService = auditService;



        }
        public List<POSTableDto> GetTablesByFloorId(long FloorId)
        {

            List<POSTable> TablesList = _posTableRepos.GetAll().Where(x => x.FloorId == FloorId && x.IsDeleted != true).ToList();
            if (TablesList.Count > 0 && TablesList != null)
            {

                var result = _mpper.Map<List<POSTableDto>>(TablesList);
                return result;

            }
            return null;
        }
        public virtual async Task<ResponseResult<List<ReservedTablesDto>>> GetReservedTablesByFloorId(long floorId)
        {
            var sp = "SP_Get_Reserved_Tables_By_Floor_Id";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);


            var result = _posTableRepos.Excute<ReservedTablesDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@floorId",
                    Value = floorId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                   new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,

                }

            }, true);

            return result;
        }


    }
}
