using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using Common.Repositories;
using Common.Exceptions;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCardDto.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ItemGroupsCardController : MainController<ItemGroupsCard, ItemGroupsCardDto, long>
    {
        private IItemGroupsCardRepository _itemGroupsCardRepository { get; set; } 
        public ItemGroupsCardController(GMappRepository<ItemGroupsCard, ItemGroupsCardDto, long> mainRepos, IItemGroupsCardRepository itemGroupsCardRepository) : base(mainRepos)
        {
            _itemGroupsCardRepository = itemGroupsCardRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<ItemGroupsCardTreeDto>>> ShowTree([FromQuery] GetAllItemGroupsCardTree query)
        {
            return Ok(await _itemGroupsCardRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(long? parentId)
        {
            return Ok(await _itemGroupsCardRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<ItemGroupsCardDto>> Add([FromBody] ItemGroupsCardDto command)
        {
            return Ok(await _itemGroupsCardRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<ItemGroupsCardDto>> Edit([FromBody] ItemGroupsCardDto command)
        {

            return Ok(await _itemGroupsCardRepository.Update(command));
        }
        [HttpGet("deleteItemGroupsCard")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<ItemGroupsCardDto>> Delete(long id) 
        {
            return Ok(await _itemGroupsCardRepository.DeleteItemGroupsCard(new DeleteItemGroupsCard() { Id = id }));
        } 
    }
}