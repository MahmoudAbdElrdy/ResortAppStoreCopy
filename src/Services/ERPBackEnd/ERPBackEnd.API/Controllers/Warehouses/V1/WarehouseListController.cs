using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class WarehouseListsController : MainController<InventoryList, WareHouseListDto, long>
    {
        private IWareHouseListRepository _repository;
        private IBillRepository _billRepository; 

        public WarehouseListsController(GMappRepository<InventoryList, WareHouseListDto, long> mainRepos, IMapper mapper,
            IWareHouseListRepository repository, IBillRepository billRepository) : base(mainRepos) 
        {
            _repository = repository;
            _billRepository = billRepository;

        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<WareHouseListDto> Create([FromBody] WareHouseListDto input)
        {
            return await _repository.CreateWareHouseList(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<WareHouseListDto> Update([FromBody] WareHouseListDto input)
        {
            return await _repository.UpdateWareHouseList(input);
        }
        [HttpGet]
        [Route("GetBillItemByItemId")]
        public async Task<WarehouseListsDetailDto> GetById([FromQuery] WhareHouseInput input) 
        {
            var res = await _billRepository.GetBillItemByItemId(input);
            return res;
        }
        [HttpPost]
        [Route("getBillItemByStoreId")]
        public async Task<List<WarehouseListsDetailDto>> GetById([FromBody] WhareHouseInputStoreId input)
        {
            var res = await _billRepository.GetBillItemByStoreId(input.StoreId, input.billDate);
            return res;
        }
        [HttpGet]
        [Route("GetWarehouseListsDetail")]
        public async Task<List<WarehouseListsDetailDto>> GetById([FromQuery] WhareHouseListInput input)  
        {
            var res = await _repository.GetWarehouseListsDetail(input); 
            return res;
        }
        public override async Task<WareHouseListDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        [HttpDelete("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }
        [HttpPost]
        [Route("getItemsByItemCodes")]
        public async Task<List<WarehouseListsDetailDto>> GetById([FromBody] WhareHouseListInputCode input)
        {
            var res = await _billRepository.GetItemsByItemCodes(input);
            return res;
        }
        [HttpGet]
        [Route("getInventoryDeterminantList")]
        public async Task<InventoryDynamicDeterminantList> GetById([FromQuery] Application.Warehouses.ItemCard.Dto.InventoryDynamicDeterminantInput input)
        {
            return await _repository.GetDynamicDeterminantList(input);
        }
    }
}
