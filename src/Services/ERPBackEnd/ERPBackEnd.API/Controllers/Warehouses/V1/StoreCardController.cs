using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Exceptions;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Dto;
using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class StoreCardController : MainController<StoreCard, StoreCardDto, long>
    {
        private IStoreCardRepository _storeCardRepository { get; set; } 
        public StoreCardController(GMappRepository<StoreCard, StoreCardDto, long> mainRepos, IStoreCardRepository storeCardRepository) : base(mainRepos)
        {
            _storeCardRepository = storeCardRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<StoreCardTreeDto>>> ShowTree([FromQuery] GetAllStoreCardTree query)
        {
            return Ok(await _storeCardRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(long? parentId)
        {
            return Ok(await _storeCardRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<StoreCardDto>> Add([FromBody] StoreCardDto command)
        {
            return Ok(await _storeCardRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<StoreCardDto>> Edit([FromBody] StoreCardDto command)
        {

            return Ok(await _storeCardRepository.Update(command));
        }
        [HttpGet("deleteStoreCard")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<StoreCardDto>> Delete(long id)  
        {
            return Ok(await _storeCardRepository.DeleteStoreCard(new DeleteStoreCard() { Id = id }));
        }
        [HttpGet("getStoresInRefernces")]

        public async Task<ResponseResult<List<StoreCardDto>>> GetStoresInRefernces()
        {
            return await _storeCardRepository.ExecuteGetStoresInRefernces();
        }
        [HttpGet("getStoresInDocument")]

        public async Task<ResponseResult<List<StoreCardDto>>> GetStoresInDocument()
        {
            return await _storeCardRepository.ExecuteGetStoresInRefernces();
        }
    }
}