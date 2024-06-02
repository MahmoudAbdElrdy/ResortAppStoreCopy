using AutoMapper;
using Common.Constants;
using Common.Entity;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Services.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ResortAppStore.Repositories;
using Sentry;
using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.Json.Nodes;
using System.Text.Json;
using Newtonsoft.Json;

namespace Common.Repositories
{
    public class GMappRepository<TEntity, TEntityDto, TKey>
        where TEntity : class
        where TEntityDto : class
    {
        private readonly IGRepository<TEntity> _mainRepos;
        private readonly IMapper _mapper;
        private readonly DeleteService _deleteService;


        public GMappRepository(IGRepository<TEntity> mainRepos, IMapper mapper, DeleteService deleteService)
        {
            _mainRepos = mainRepos;
            _mapper = mapper;
            _deleteService = deleteService;
        }
        public string PermissionSpName { get; set; }
        public TEntityDto MapToEntityDto(TEntity entity)
        {
            return _mapper.Map<TEntityDto>(entity);
        }
        private List<TEntityDto> MapToEntityDto(List<TEntity> entities)
        {

            return _mapper.Map<List<TEntityDto>>(entities);
        }
        private TEntity MapToEntity(TEntityDto createInput)
        {
            return _mapper.Map<TEntity>(createInput);
        }

        private void MapToEntity(TEntityDto updateInput, TEntity entity)
        {
            _mapper.Map(updateInput, entity);
        }
        public virtual async Task<PaginatedList<TEntityDto>> GetAllIncluding(Paging paging, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var entities = _mainRepos.GetAllIncluding(propertySelectors).Where(u => EF.Property<bool>(u, "IsDeleted") == false);

            //var entitiesQuery = entities.Skip((paging.PageIndex - 1) * paging.PageSize)
            //     .Take(paging.PageSize).ToList();

            var totalCount = await entities.CountAsync();

            var transferReasonDto = MapToEntityDto(entities.ToList());

            return new PaginatedList<TEntityDto>(transferReasonDto,
              totalCount,
              paging.PageIndex,
              paging.PageSize);
        }

        public virtual async Task<PaginatedList<TEntityDto>> GetAll(Paging paging, string includeProperties = "")
        {
            var entities = _mainRepos.GetAllIncluding(includeProperties);

            //var entitiesQuery = entities.Skip((paging.PageIndex - 1) * paging.PageSize)
            //     .Take(paging.PageSize).ToList();
            var entitiesQuery = entities.ToList();
            var totalCount = await entities.CountAsync();

            var transferReasonDto = MapToEntityDto(entitiesQuery.ToList());

            return new PaginatedList<TEntityDto>(transferReasonDto,
              totalCount,
              paging.PageIndex,
              paging.PageSize);
        }
        public virtual async Task<TEntityDto> FirstOrDefault(object id, string includeProperties = "")
        {
            var entity = await _mainRepos.GetAllIncluding(includeProperties)
                .Where(u => EF.Property<object>(u, "Id") == id && EF.Property<bool>(u, "IsDeleted") == false)
                .FirstOrDefaultAsync();
            var result = MapToEntityDto(entity);

            return result;

        }
        public virtual async Task<List<TEntityDto>> GetDDL()
        {
            var entities = await _mainRepos.GetAllListAsync();

            var transferReasonDto = MapToEntityDto(entities);

            return new List<TEntityDto>(transferReasonDto);
        }

        public virtual async Task ExecuteCreate(TEntity entity)
        {
            await _mainRepos.InsertAsync(entity);
            await _mainRepos.SaveChangesAsync();
        }

        public virtual async Task ExecuteCreateWithPermission(TEntity entity, string roleId,  string sp)
        {
            await _mainRepos.InsertAsync(entity);
            await _mainRepos.SaveChangesAsync();

            var id = entity.GetType().GetProperty("Id");
            _mainRepos.Excute(sp , new List<SqlParameter>() { 
                new SqlParameter(){
                    ParameterName = "@RoleId",
                    Value = roleId,


                },
                new SqlParameter(){
                    ParameterName = "@TypeId",
                    Value =id.GetValue(entity),
                }
            },true);
        }
        public virtual async Task<TEntityDto> Create(TEntityDto input)
        {
            var code = input.GetType().GetProperty("Code");

            if (code != null)
            {
                var valueCode = code.GetValue(input);

                var existCode = await _mainRepos.GetAllAsNoTracking().AnyAsync((u => EF.Property<string>(u, "Code") == valueCode && EF.Property<bool>(u, "IsDeleted") == false));

                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }
            var entity = MapToEntity(input);

            var createdBy = entity.GetType().GetProperty("CreatedBy");

            if (createdBy != null)
            {
                createdBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            var id = entity.GetType().GetProperty("Id");

            if (id != null)
            {
                id.SetValue(entity, null);
            }
            await ExecuteCreate(entity);

            var result = MapToEntityDto(entity);

            return result;
        }


        public virtual async Task<TEntityDto> CreateWithPermission(TEntityDto input, string roleId)
        {
            var code = input.GetType().GetProperty("Code");

            if (code != null)
            {
                var valueCode = code.GetValue(input);

                var existCode = await _mainRepos.GetAllAsNoTracking().AnyAsync((u => EF.Property<string>(u, "Code") == valueCode && EF.Property<bool>(u, "IsDeleted") == false));

                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }
            var entity = MapToEntity(input);

            var createdBy = entity.GetType().GetProperty("CreatedBy");

            if (createdBy != null)
            {
                createdBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            var id = entity.GetType().GetProperty("Id");

            if (id != null)
            {
                id.SetValue(entity, null);
            }
            await ExecuteCreateWithPermission(entity, roleId, PermissionSpName);

            var result = MapToEntityDto(entity);

            return result;
        }

        public virtual async Task<TEntityDto> CreateTEntity(TEntity input)
        {


            await ExecuteCreate(input);

            var result = MapToEntityDto(input);

            return result;
        }
        public async Task<TEntity> ExecuteGetById(TKey id)
        {

            return await _mainRepos.FirstOrDefaultAsync(id);
        }
        public async Task<TEntity> ExecuteGetById(object id)
        {
            var res = await _mainRepos.FirstOrDefaultAsync(id);
            if (res == null)
            {
                throw new UserFriendlyException("NotFound");
            }
            return res;
        }
        public virtual async Task<TEntityDto> Get(TKey id)
        {
            var entity = await ExecuteGetById(id);
            return MapToEntityDto(entity);
        }
        public virtual async Task ExecuteUpdateAsync(TEntityDto input, TEntity entity)
        {
            MapToEntity(input, entity);
            await _mainRepos.SaveChangesAsync();
        }
        public virtual async Task ExecuteUpdateEntity(TEntityDto input, TEntity entity)
        {

            await _mainRepos.SaveChangesAsync();
        }
        public virtual async Task<TEntityDto> Update(TEntityDto input)
        {
            var id = input.GetType().GetProperty("Id");

            var value = id.GetValue(input);

            var entity = await ExecuteGetById(value);

            var code = input.GetType().GetProperty("Code");

            if (code != null)
            {
                var valueCode = code.GetValue(input);

                var existCode = await _mainRepos.GetAllAsNoTracking().AnyAsync(
                    (u => EF.Property<object>(u, "Id") != value && EF.Property<string>(u, "Code") == valueCode && EF.Property<bool>(u, "IsDeleted") == false));

                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");

            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.Now);
            }
            var updateBy = entity.GetType().GetProperty("UpdateBy");

            if (updateBy != null)
            {
                updateBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }

            await ExecuteUpdateAsync(input, entity);

            return MapToEntityDto(entity);
        }
        public virtual async Task<TEntityDto> UpdateWithoutCheckCode(TEntityDto input)
        {
            var id = input.GetType().GetProperty("Id");

            var value = id.GetValue(input);

            var entity = await ExecuteGetById(value);
           
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");

            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.Now);
            }
            var updateBy = entity.GetType().GetProperty("UpdateBy");

            if (updateBy != null)
            {
                updateBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }

            await ExecuteUpdateAsync(input, entity);

            return MapToEntityDto(entity);
        }

        public virtual async Task<TEntityDto> UpdateEntity(TEntityDto input)
        {
            var id = input.GetType().GetProperty("Id");



            var value = id.GetValue(input);

            var entity = await ExecuteGetById(value);

            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");

            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.Now);
            }
            var updateBy = entity.GetType().GetProperty("UpdateBy");

            if (updateBy != null)
            {
                updateBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }

            await ExecuteUpdateAsync(input, entity);


            return MapToEntityDto(entity);
            //var id = input.GetType().GetProperty("Id");
            //var value = id.GetValue(input);
            ////var entity = await ExecuteGetById(value);
            ////var dto = MapToEntityDto(entity);

            //await _mainRepos.SaveChangesAsync();

            //return MapToEntityDto(input);
        }
        public virtual async Task<int> SoftDeleteAsync(TKey id, string tableName = "", string idName = "")
        {
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(idName))
            {
                var isDeleted = await _deleteService.ScriptCheckDelete(tableName, idName, id);

                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-record");
            }

            return await _mainRepos.SoftDeleteAsync(id);
        }
        public virtual async Task<int> SoftDeleteAsync(DeleteEntity input)
        {


            if (!string.IsNullOrEmpty((string?)input.TableName) && !string.IsNullOrEmpty((string?)input.IdName))
            {
                var isDeleted = await _deleteService.ScriptCheckDelete(input.TableName, input.IdName, input.Id);

                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-record");
            }

            return await _mainRepos.SoftDeleteAsync(input.Id);
        }
        public async Task<int> SoftDeleteListAsync(List<object> ids, string tableName = "", string idName = "")
        {
            foreach (var id in ids)
            {
                if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(idName))
                {

                    var isDeleted = await _deleteService.ScriptCheckDelete(tableName, idName, id);

                    if (!isDeleted)
                        throw new UserFriendlyException("can't-delete-some-records");
                }

                await _mainRepos.SoftDeleteListAsync(id);
            }
            var res = await _mainRepos.SaveChangesAsync();
            return res;
        }

        /// Edit By Kadnil
        public async Task<int> SoftDeleteListAsync(DeleteListEntity input)
        {

            foreach (var id in input.Ids)
            {
                string deletedId = "";
                if (!string.IsNullOrEmpty(input.TableName) && !string.IsNullOrEmpty(input.IdName))
                {
                    // Directly use the ToString method if id is a JObject
                    
                    if (id is JObject jObject)
                    {
                        // Convert JObject to string before deserialization
                        var jsonString = id.ToString();
                        if(jsonString is not null)
                        {
                            var myObject = JsonConvert.DeserializeObject<DeletedObject>(jsonString) ;
                            deletedId = myObject.Id;
                        }
                     
                    }
                    else if (id is string idStr)
                    {
                        // Assume id is directly a string if not a JObject
                        deletedId = idStr;
                    }
                    // The rest of your logic...
                    var isDeleted = await _deleteService.ScriptCheckDelete(input.TableName, input.IdName, deletedId);

                    if (!isDeleted)
                        throw new UserFriendlyException("can't-delete-some-records");
                }

                await _mainRepos.SoftDeleteListAsync(deletedId);
            }
            var res = await _mainRepos.SaveChangesAsync();
            return res;
        }
        public virtual async Task<int> ScriptCheckDeleteWithDetails(TKey id, List<string> details, string tableName = "", string idName = "")
        {
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(idName))
            {
                var isDeleted = await _deleteService.ScriptCheckDeleteWithDetails(tableName, idName, id, details);

                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-record");
            }

            return await _mainRepos.SoftDeleteAsync(id);
        }
        public async Task<int> ScriptCheckDeleteWithDetails(List<object> ids, List<string> details, string tableName = "", string idName = "")
        {
            foreach (var id in ids)
            {
                if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(idName))
                {

                    var isDeleted = await _deleteService.ScriptCheckDeleteWithDetails(tableName, idName, id, details);

                    if (!isDeleted)
                        throw new UserFriendlyException("can't-delete-some-records");
                }

                await _mainRepos.SoftDeleteListAsync(id);
            }
            var res = await _mainRepos.SaveChangesAsync();
            return res;
        }


        public virtual string LastCode()
        {
            var resLastCode = _mainRepos.LastCode();

            if (resLastCode == null)
                return GenerateRandom.GetSerialCode(Convert.ToInt64("0"));

            var codeRes = resLastCode.GetType().GetProperty("Code");

            var value = codeRes.GetValue(resLastCode);

            var code = GenerateRandom.GetSerialCode(Convert.ToInt64(resLastCode != null ? value : "0"));

            return code;
        }


    }
    public class DeletedObject
    {
        public string Id { get; set; }
        public bool IsChecked { get; set; }
    }
}
