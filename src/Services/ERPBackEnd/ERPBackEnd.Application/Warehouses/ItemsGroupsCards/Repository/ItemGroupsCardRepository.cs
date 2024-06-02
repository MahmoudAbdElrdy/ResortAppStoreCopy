using AutoMapper;
using Common.Extensions;
using Common.Helper;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCardDto.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.ItemsGroupsCards.Repository 
{
    public class ItemGroupsCardRepository : GMappRepository<ItemGroupsCard, ItemGroupsCardDto, long>, IItemGroupsCardRepository
    {
        private readonly IGRepository<ItemGroupsCard> _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly DeleteService _deleteService;
        List<long> itemsIds = new List<long>();
        public ItemGroupsCardRepository(IGRepository<ItemGroupsCard> mainRepos, IMapper mapper, DeleteService deleteService, IAuditService auditService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
            _auditService = auditService;
            _deleteService = deleteService;
        }
        public  async Task<ItemGroupsCardDto> Create(ItemGroupsCardDto request) 
        {
            var entity = _mapper.Map<ItemGroupsCard>(request);

            var parent = await _context.FirstOrDefaultAsync(c => c.Id == request.ParentId);

            var parentList = _context.GetAllList(c => c.Id == request.ParentId).ToList();
            if (request.Code == _auditService.Code)
            {
                var max = (parentList.Select(x => Convert.ToInt32(x.Code)).DefaultIfEmpty(0).Max());
               
                var code = GenerateRandom.GenerateIdInt64(parent.Code, max, parent.LevelId + 1);
                if (request.Code != code)
                {
                    var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);

                    entity.Code = code;
                }
            }
            else
            {
                var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);
                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }
            var maxId = _context.GetAllList().Max(e => (int?)e.Id) ?? 0;
            entity.Id = maxId + 1;
            entity.TreeId = parent != null ? parent.TreeId + GenerateRandom.GenerateTreeId(entity.Id) : GenerateRandom.GenerateTreeId(entity.Id);
            entity.LevelId = parent != null ? parent.LevelId + 1 : 0;
            await _context.InsertAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemGroupsCardDto>(entity);
        }
        public  async Task<ItemGroupsCardDto> Update(ItemGroupsCardDto request)
        { 
            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.Id);
          
            if (entityDb == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Id != entityDb.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

            entityDb = _mapper.Map(request, entityDb);
            var parent = await _context.FirstOrDefaultAsync(c => c.Id == request.ParentId);
            entityDb.TreeId = parent != null ? parent.TreeId + GenerateRandom.GenerateTreeId(entityDb.Id) : GenerateRandom.GenerateTreeId(entityDb.Id);
            entityDb.LevelId = parent != null ? parent.LevelId + 1 : 0;

            await _context.UpdateAsync(entityDb);
            await _context.SaveChangesAsync();



            return _mapper.Map<ItemGroupsCardDto>(entityDb);
        }
       
        public async Task<List<ItemGroupsCardTreeDto>> GetTrees(GetAllItemGroupsCardTree request) 
        {

            GetListIdsOfItems(request.SelectedId);
            var tree = _context.GetAllIncluding(c=>c.ItemCards).Where(p => p.ParentId == null && p.IsDeleted != true).ToList().
              Select(p => new ItemGroupsCardTreeDto
              {
                  expanded = itemsIds.Find(x => x == p.ParentId && !p.IsDeleted) != 0 ? true : false,
                  IsActive=p.IsActive,
                  Id = p.Id,
                  ParentId = p.ParentId,
                  NameAr = p.NameAr,
                  NameEn = p.NameEn,
                  Code = p.Code,
                  LevelId = p.LevelId,
                  TreeId = p.TreeId,
                  ItemCards = _mapper.Map<List<ItemCardDto>>(p?.ItemCards?.ToList()).Where(x=>!x.IsDeleted),



                  children = _context.GetAllList().FirstOrDefault(c => c.ParentId == p.Id && !p.IsDeleted) != null ?
                 GetItemGroupsCardChilds(p.Id) : null
              }).OrderBy(c => c.Code).ToList();

            return tree;
        }
        public async Task<string> GetLastCode(GetLastCode request) 
        {

            var parent = _context.GetAllList(c => c.ParentId == request.ParentId && !c.IsDeleted).ToList();

            var checkParentFound = _context.FirstOrDefault(c => c.Id == request.ParentId && !c.IsDeleted);

            var max = (parent.Select(x => Convert.ToInt32(x.Code)).DefaultIfEmpty(0).Max());


            var levelId = checkParentFound == null ? 0 : checkParentFound.LevelId;
            var codeParent = checkParentFound == null ? "" : checkParentFound.Code;

            return GenerateRandom.GenerateIdInt64(codeParent, max, levelId + 1);


        }
        public async Task<ItemGroupsCardDto> DeleteItemGroupsCard(DeleteItemGroupsCard request) 
        {
            var itemGroupsCard = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (itemGroupsCard == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var isDeleted = await _deleteService.ScriptCheckDeleteWithDetails("ItemGroupsCard", "Id", request.Id, new List<string> { "ItemCards" });
            if (!isDeleted)
                throw new UserFriendlyException("can't-delete-record");
           
            await SoftDeleteRecursiveAsync(request.Id);
            await _context.SaveChangesAsync();
            return _mapper.Map<ItemGroupsCardDto>(itemGroupsCard);
        }
        private async Task SoftDeleteRecursiveAsync(long parentId)
        {
            // Set the IsDeleted flag for this item
            var item = await _context.FirstOrDefaultAsync(c => c.Id == parentId && !c.IsDeleted);
            item.IsDeleted = true;
           
            var children = _context.GetAllList().Where(x => x.ParentId == parentId && !x.IsDeleted).ToList();
            foreach (var child in children)
            {
                var isDeleted = await _deleteService.ScriptCheckDelete("ItemGroupsCard", "Id", child.Id);
                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-record");
                await SoftDeleteRecursiveAsync(child.Id);
            }
        }
        private IEnumerable<ItemGroupsCardTreeDto> GetItemGroupsCardChilds(long ParentId)
        {

            var childs = _context.GetAllIncluding(c=>c.ItemCards).Where(p => p.ParentId == ParentId && p.IsDeleted != true).ToList();
            foreach (var p in childs)
            {
                yield return new ItemGroupsCardTreeDto
                {
                    expanded = itemsIds.Find(x => x == p.Id && !p.IsDeleted) != 0 ? true : false,

                    Id = p.Id,
                    ParentId = p.ParentId,
                    NameAr = p.NameAr,
                    NameEn = p.NameEn,
                    Code = p.Code,
                    LevelId = p.LevelId,
                    TreeId = p.TreeId,
                   
                    IsActive = p.IsActive,
                    
                    ItemCards= _mapper.Map<List<ItemCardDto>>(p?.ItemCards?.ToList()).Where(x => !x.IsDeleted),
                    
                    children = _context.GetAllList().FirstOrDefault(c => c.ParentId == p.Id && !p.IsDeleted) != null ?
                
                    GetItemGroupsCardChilds(p.Id) : null,
                };

            }
        }
        void GetListIdsOfItems(Int64? id)
        {
            var item = _context.GetAllList().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                itemsIds.Add(item.Id);
                if (item.ParentId != null)
                {
                    GetListIdsOfItems(item.ParentId.Value);
                }
            }

        }
    }
}
