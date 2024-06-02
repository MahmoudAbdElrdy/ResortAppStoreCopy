using AutoMapper;
using Common.Exceptions;
using Common.Infrastructures;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]
     [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class MainController<TEntity, TEntityDto, TKey> : ControllerBase
        where TEntity : class
        where TEntityDto : class

    {
        private readonly GMappRepository<TEntity, TEntityDto, TKey> _mainRepos;
        private readonly IMapper _mapper; 
       
        public MainController(GMappRepository<TEntity, TEntityDto, TKey> mainRepos, IMapper mapper) 
        {
            _mainRepos = mainRepos;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<PageList<TEntityDto>>> GetAll(Paging paging)
        {

            return Ok(await _mainRepos.GetAll(paging));

        }

        

        [HttpGet]
        [Route("GetById")]
        public async Task<TEntityDto> GetById(TKey id)
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
        public async Task<TEntityDto> Create([FromBody] TEntityDto input)
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
        public async Task<TEntityDto> Update([FromBody] TEntityDto input,TKey id) 
        {

            return await _mainRepos.Update(input,id);

        }
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public  Task Delete(TKey id)
        {
          return   _mainRepos.SoftDeleteAsync(id);
        }


    }
}
