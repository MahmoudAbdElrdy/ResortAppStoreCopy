using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.BankAccounts.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.BankAccountes.Repository
{
    public class BankAccountRepository : GMappRepository<BankAccount, BankAccountDto, long>,IBankAccountRepository
    {
        private readonly IGRepository<BankAccount> _mainRepos;
        private IMapper _mpper;
        public BankAccountRepository(IGRepository<BankAccount> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _mainRepos = mainRepos;
            _mpper = mapper;
        }

       
        public  async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "BankAccounts", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "BankAccounts", "Id");
        }
      
        public async Task<BankAccountDto> FirstInclude(long id)
        {
            var item = await _mainRepos.GetAllIncluding(c => c.Account).FirstOrDefaultAsync(c => c.Id == id);
            var result = _mpper.Map<BankAccountDto>(item);
            return result;
        }

        public Task<BankAccountDto> CreateEntity(BankAccountDto input)
        {
            var entity = _mpper.Map<BankAccount>(input);
            entity.Account = null;
            return base.CreateTEntity(entity);
        }
        public async Task<BankAccountDto> UpdateEntity(BankAccountDto input)
        {
            var entity = await ExecuteGetById(input.Id);
            _mpper.Map(input, entity);
            entity.Account = null;
            await _mainRepos.SaveChangesAsync();
            return MapToEntityDto(entity);
        }
       
        public Task<PaginatedList<BankAccountDto>> GetAllIncluding(Paging paging)
        {

            return base.GetAllIncluding(paging, x => x.Account);
        }

    }
}
