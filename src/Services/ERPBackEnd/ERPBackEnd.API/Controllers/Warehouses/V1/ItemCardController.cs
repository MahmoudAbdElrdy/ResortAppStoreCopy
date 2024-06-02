using AutoMapper;
using Common.BaseController;
using Common.Constants;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ItemCardController : MainController<ItemCard, ItemCardDto, long>
    {
        private IItemCardRepository _repository;

        public ItemCardController(
            GMappRepository<ItemCard, ItemCardDto, long> mainRepos, IMapper mapper,
            IItemCardRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        //[HttpPost("create")]
        //[SuccessResultMessage("createSuccessfully")]
        public override async Task<ItemCardDto> Create([FromBody] ItemCardDto input)
        {
            return await _repository.CreateItemCard(input);
        }
        //[HttpPost("update")]
        //[SuccessResultMessage("editSuccessfully")]
        public override async Task<ItemCardDto> Update([FromBody] ItemCardDto input)
        {
            return await _repository.UpdateItemCard(input);
        }
        public override async Task<ItemCardDto> GetById(long id)
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
        #region getLastCodeByItemGroupId 
        [HttpGet("getLastCodeByItemGroupId")]
        public IActionResult getLastCodeByItemGroupId(long ItemGroupId)
        {
            string Lastcode = _repository.getLastCodeByItemGroupId(ItemGroupId);

            return Ok(new { message = "Last Code:", data = Lastcode });
        }

        [HttpGet("checkHaveDeterminant")]
        
        public Task<bool> Update(long id)
        {

            return _repository.CheckHaveDeterminant(id);
        }

        #endregion
        //  Task<BillDynamicDeterminantList> GetBillDynamicDeterminantList(BillDynamicDeterminantInput input);
        // Task<BillDynamicDeterminantList> InsertBillDynamicDeterminant(InsertBillDynamicDeterminant input);
        [HttpGet("calculateItemCardBalances")]

        public async Task<ResponseResult<List<CalculateItemCardBalanceDto>>> CalculateItemCardBalances(long ItemCardId, long StoreId, DateTime? BillDate)
        {
            return await _repository.CalculateItemBalances(ItemCardId, StoreId,BillDate);
        }
        [HttpGet("getItemsCardInRefernces")]

        public async Task<ResponseResult<List<ItemCardDto>>> GetItemsCardInRefernces()
        {
            return await _repository.ExecuteGetItemsCardInRefernces();
        }
        [HttpGet("getItemsByItemGroupId")]

        public  List<ItemCardDto> GetAll(long ItemGroupId)
        {
            return  _repository.GetItemsByItemGroupId(ItemGroupId);
        }
        [HttpGet("getItemsByItemSearch")]

        public List<ItemCardDto> GetAll(string Item)
        {
            return _repository.GetPOSItemsByItemSearch(Item);
        }

        [HttpGet("getPOSItems")]

        public List<ItemCardDto> GetAll()
        {
            return _repository.GetPOSItems();
        }
        [HttpGet("updateGPCCode")]
        [SuccessResultMessage("editSuccessfully")]

        public async Task Update(long ItemGroupId, string GPCCode)
        {
            await _repository.UpdateGPCCode(ItemGroupId, GPCCode);
        }
        [HttpPost("upload")]
        //[SuccessResultMessage("uploadSuccessfully")]
        public async Task<ActionResult<int>> Create([FromBody] List<object> ids)
        {
            return await _repository.UploadItems(ids);
        }
    }
}
