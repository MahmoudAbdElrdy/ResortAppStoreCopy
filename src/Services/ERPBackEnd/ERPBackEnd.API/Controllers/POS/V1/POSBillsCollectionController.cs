using AutoMapper;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.POS;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Repository;
using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using Common.BaseController;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using Common.Exceptions;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class POSBillsCollectionController : MainController<POSBillsCollection, POSBillsCollection, long>
    {
        private IPOSBillsCollectionRepository _repository;
        private IBillRepository _billrepository;

        public POSBillsCollectionController(
          GMappRepository<POSBillsCollection, POSBillsCollection, long> mainRepos, IMapper mapper, IBillRepository billrepository, IPOSBillsCollectionRepository repository
       ) : base(mainRepos)
        {
            _repository = repository;
            _billrepository = billrepository;

        }
    


        [HttpPost("DoPOSBillsCollection")]
        [SuccessResultMessage("createSuccessfully")]

        public async Task<ResponseResult> DoPOSBillsCollection(POSBillsCollection _POSBillsCollection)
        {
            return await _repository.ExecuteGetPOSBillsCollection(_POSBillsCollection);
        }




    }
}
