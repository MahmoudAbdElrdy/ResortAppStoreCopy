using AutoMapper;
using Common.Extensions;
using Common.Helper;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Repository
{
    public class AccountRepository : GMappRepository<Account, AccountDto, long>, IAccountRepository
    {
        private readonly IGRepository<Account> _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly DeleteService _deleteService;
        List<long> itemsIds = new List<long>();
        private readonly IDapperRepository<AccountBalanceDto> _query;

        public AccountRepository(IGRepository<Account> mainRepos, IMapper mapper, DeleteService deleteService, IAuditService auditService, IDapperRepository<AccountBalanceDto> query) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
            _auditService = auditService;
            _deleteService = deleteService;
            _query = query;
        }

        public async Task<AccountDto> Create(AccountDto request)
        {
            var entity = _mapper.Map<Account>(request);
            entity.CreatedAt = System.DateTime.UtcNow;
            entity.IsDeleted = false;

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
            var maxId = _context.GetAllList().Select(x => Convert.ToInt32(x.Id)).DefaultIfEmpty(0).Max();

            entity.Id = (maxId + 1).ToString();

            entity.TreeId = parent != null ? parent.TreeId + GenerateRandom.GenerateTreeId(Convert.ToInt64(entity.Id)) : GenerateRandom.GenerateTreeId(Convert.ToInt64(entity.Id));

            entity.LevelId = parent != null ? parent.LevelId + 1 : 0;
            if (parent != null && parent.IsLeafAccount == true)
            {
                var isDeleted = await _deleteService.ScriptCheckDelete("Accounts", "Id", parent.Id);
                if (!isDeleted)
                    throw new UserFriendlyException("canChangeToParent");
                parent.IsLeafAccount = false;
                await _context.UpdateAsync(parent);
            }

            await _context.InsertAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDto>(entity);
        }
        public async Task<AccountDto> Update(AccountDto request)
        {
            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (entityDb == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            entityDb.UpdatedAt = System.DateTime.UtcNow;
       
            var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Id != entityDb.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");
            if (entityDb.IsLeafAccount != request.IsLeafAccount)
            {
                var isDeleted = await _deleteService.ScriptCheckDelete("Accounts", "Id", request.Id);
                if (!isDeleted)
                    throw new UserFriendlyException("Can Not Change Leaf Account");

                var childs = _context.GetAllList().Where(p => p.IsDeleted != true && p.IsActive == true && p.ParentId == request.Id).ToList();

                if (childs.Count > 0 && childs != null)
                {
                    throw new UserFriendlyException("Can Not Change Leaf Account");
                }
            }

            entityDb = _mapper.Map(request, entityDb);
            var parent = await _context.FirstOrDefaultAsync(c => c.Id == request.ParentId);
            entityDb.TreeId = parent != null ? parent.TreeId + GenerateRandom.GenerateTreeId(Convert.ToInt64(entityDb.Id)) : GenerateRandom.GenerateTreeId(Convert.ToInt64(entityDb.Id));

            entityDb.LevelId = parent != null ? parent.LevelId + 1 : 0;

            if (request.ParentId != null)
            {

            }

            await _context.UpdateAsync(entityDb);
            await _context.SaveChangesAsync();



            return _mapper.Map<AccountDto>(entityDb);


        }

        public async Task<List<AccountTreeDto>> GetTrees(GetAllAccountTreeCommand request)
        {

            GetListIdsOfItems(request.SelectedId);
            var tree = _context.GetAllList().Where(p => p.ParentId == null && p.IsDeleted != true).ToList().
              Select(p => new AccountTreeDto
              {
                  expanded = itemsIds.Find(x => x == Convert.ToInt64(p.ParentId) && !p.IsDeleted) != 0 ? true : false,
                  Id = p.Id,
                  ParentId = p.ParentId,
                  NameAr = p.NameAr,
                  NameEn = p.NameEn,
                  Code = p.Code,
                  LevelId = p.LevelId,
                  TreeId = p.TreeId,
                  IsLeafAccount = p.IsLeafAccount,
                  AccountType = p.AccountType,
                  children = _context.GetAllList().FirstOrDefault(c => c.ParentId == p.Id && !p.IsDeleted) != null ?
                 GetAccountChilds(p.Id) : null
              }).OrderBy(c => c.TreeId).ToList();

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
        public async Task<AccountDto> DeleteAccount(DeleteAccountCommand request)
        {
            var Account = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (Account == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var isDeleted = await _deleteService.ScriptCheckDelete("Accounts", "Id", request.Id);
            if (!isDeleted)
                throw new UserFriendlyException("can't-delete-record");
            //  Account.IsDeleted = true;
            //  await _context.SoftDeleteAsync(Account);
            await SoftDeleteRecursiveAsync(request.Id);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountDto>(Account);
        }
        private async Task SoftDeleteRecursiveAsync(string parentId)
        {
            // Set the IsDeleted flag for this item
            var item = await _context.FirstOrDefaultAsync(c => c.Id == parentId && !c.IsDeleted);
            item.IsDeleted = true;
          

            // Recursively delete all child items
            var children = _context.GetAllList().Where(x => x.ParentId == parentId && !x.IsDeleted).ToList();
            foreach (var child in children)
            {
                var isDeleted = await _deleteService.ScriptCheckDelete("Accounts", "Id", child.Id);
                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-record");
                await SoftDeleteRecursiveAsync(child.Id);
            }
        }
        private IEnumerable<AccountTreeDto> GetAccountChilds(string ParentId)
        {

            var childs = _context.GetAllList().Where(p => p.ParentId == ParentId && p.IsDeleted != true).OrderBy(c=>c.TreeId).ToList();

            foreach (var p in childs)
            {
                yield return new AccountTreeDto
                {
                    expanded = itemsIds.Find(x => x == Convert.ToInt64(p.ParentId) && !p.IsDeleted) != 0 ? true : false,

                    Id = p.Id,
                    ParentId = p.ParentId,
                    NameAr = p.NameAr,
                    NameEn = p.NameEn,
                    Code = p.Code,
                    LevelId = p.LevelId,
                    TreeId = p.TreeId,
                    IsLeafAccount = p.IsLeafAccount,
                    AccountType = p.AccountType,

                    children = _context.GetAllList().FirstOrDefault(c => c.ParentId == p.Id && !p.IsDeleted) != null ?
                 GetAccountChilds(p.Id) : null,
                };

            }
        }
        void GetListIdsOfItems(string? id)
        {
            var item = _context.GetAllList().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                itemsIds.Add(Convert.ToInt64(item.Id));
                if (item.ParentId != null)
                {
                    GetListIdsOfItems(item.ParentId);
                }
            }

        }

        public async Task<IEnumerable<Account>> GetLeafAccounts(long AccountClassificationId)
        {
          
            if (AccountClassificationId == 0)
            {
                var childs = _context.GetAllList().Where(p => p.IsLeafAccount == true && p.IsDeleted != true && p.IsActive == true).ToList();
                return childs;

            }
            else
            {
                var childs = _context.GetAllList().Where(p => p.IsLeafAccount == true && p.IsDeleted != true && p.IsActive == true && p.AccountClassificationId == AccountClassificationId).ToList();
                return childs;

            }
          
        }
        public async Task<bool> IsLeafAccount(DeleteAccountCommand request)
        {
            var account = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (account == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var isDeleted = await _deleteService.ScriptCheckDelete("Accounts", "Id", request.Id);
            if (!isDeleted)
                return false;

            var childs = _context.GetAllList().Where(p => p.IsLeafAccount == true && p.IsDeleted != true && p.IsActive == true && p.ParentId == request.Id).ToList();

            if (childs.Count > 0 && childs != null)
            {
                return false;
            }


            return true;
        }
        public async Task<List<AccountBalanceDto>> GetAccountBalalnce(string accountId)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT [dbo].[fn_Get_Account_Balance] ");
            query.AppendFormat(" ('{0}') as Balance ", accountId);
            var result = await _query.QueryAsync<AccountBalanceDto>(query.ToString());
            return (List<AccountBalanceDto>)result;
        }

    }
}
