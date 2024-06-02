using Common.Entity;
using Common.Exceptions;
using Common.Infrastructures;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Common.BaseController
{
   
    [Authorization]
    public class MainController<TEntity, TEntityDto, TKey> : ControllerBase
        where TEntity : class
        where TEntityDto : class

    {
        private readonly GMappRepository<TEntity, TEntityDto, TKey> _mainRepos;
     
        public MainController(GMappRepository<TEntity, TEntityDto, TKey> mainRepos) 
        {
            _mainRepos = mainRepos;
        


        }

        [HttpGet]
        [Route("all")]
       
        public virtual async Task<ActionResult<PageList<TEntityDto>>> GetAll([FromQuery] Paging paging)
        {
         
                return  Ok(await _mainRepos.GetAll(paging));

        }
        [HttpGet("get-ddl")]
        public virtual async Task<ActionResult<List<TEntityDto>>> GetDDL() 
        {
            return Ok(await _mainRepos.GetDDL());
        }


        [HttpGet]
        [Route("getById")]
        public virtual async Task<TEntityDto> GetById(TKey id)
        {
            return await _mainRepos.Get(id);
        }

       


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">Entitty Will Added To Table</param>
        /// <param name="uniques">Unique column Name to check exist </param>
        /// /// <param name="checkAll">if true Check all unique with and Condition else check unique with or </param>
        /// <returns></returns>

      
         [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public virtual async Task<TEntityDto> Create([FromBody] TEntityDto input)
        {

            return await _mainRepos.Create(input);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">Entitty Will Added To Table</param>
        /// <param name="uniques">Unique column Name to check exist </param>
        /// /// <param name="checkAll">if true Check all unique with and Condition else check unique with or </param>
        /// <returns></returns>

        [HttpPost("edit")]
        [SuccessResultMessage("editSuccessfully")]
        public virtual async Task<TEntityDto> Update([FromBody] TEntityDto input) 
        {
            
            return await _mainRepos.Update(input);

        }
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public virtual Task Delete(TKey id)
        {
          return   _mainRepos.SoftDeleteAsync(id);
        }

        [HttpGet("getLastCode")]

        public virtual async Task<ActionResult<string>> GetLastCode()
        {
            return Ok(_mainRepos.LastCode());
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public virtual async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _mainRepos.SoftDeleteListAsync(ids);
        } 
        [HttpPost("deleteEntity")]
        [SuccessResultMessage("deleteSuccessfully")]
        public virtual async Task<ActionResult<int>> Delete([FromBody] DeleteEntity input)
        {
            return await _mainRepos.SoftDeleteAsync(input);
        }
        [HttpPost("deleteListEntity")]
        [SuccessResultMessage("deleteSuccessfully")]
        public virtual Task SoftDeleteList([FromBody] DeleteListEntity input)
        {
            return _mainRepos.SoftDeleteListAsync(input);
        }


    }
}
