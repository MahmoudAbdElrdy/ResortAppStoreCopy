using AutoMapper;
using Common.Extensions;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountClassifications.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using Sentry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.AccountClassificationes.Repository
{
    public class AccountClassificationRepository : GMappRepository<AccountClassification, AccountClassificationDto, long>, IAccountClassificationRepository
    {
        private IGRepository<AccountClassification> _mainRepos;
        IMapper _mapper;
        public AccountClassificationRepository(IGRepository<AccountClassification> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _mainRepos = mainRepos;
            _mapper = mapper;
        }
        public async Task<AccountClassificationDto> Update(AccountClassificationDto request)
        {
            var entityDb = await _mainRepos.FirstOrDefaultAsync(c => c.Id == request.Id);
            entityDb.UpdatedAt = System.DateTime.UtcNow;
         
            if (entityDb == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var existCode = await _mainRepos.GetAllAsNoTracking().AnyAsync(x => x.Id != entityDb.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

            entityDb = _mapper.Map(request, entityDb);
            var itemList = await _mainRepos.GetAllAsNoTracking().AnyAsync(c => (c.NameAr == request.NameAr || c.NameEn == request.NameEn) && c.Type == request.Type&& c.Id != request.Id);
            if (itemList)
                throw new UserFriendlyException("repeated-name");
            await _mainRepos.UpdateAsync(entityDb);
            await _mainRepos.SaveChangesAsync();



            return _mapper.Map<AccountClassificationDto>(entityDb);

        }

        public async Task<AccountClassificationDto> Create(AccountClassificationDto request)  
        {
            var entity = _mapper.Map<AccountClassification>(request);
            entity.CreatedAt = System.DateTime.UtcNow;
            entity.IsDeleted = false;
            var existCode = await _mainRepos.GetAllAsNoTracking().AnyAsync(x =>x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

       
            var itemList = await _mainRepos.GetAllAsNoTracking().AnyAsync(c => (c.NameAr == request.NameAr || c.NameEn == request.NameEn) && c.Type == request.Type);
            if (itemList)
                throw new UserFriendlyException("repeated-name");
            await _mainRepos.InsertAsync(entity);
            await _mainRepos.SaveChangesAsync();



            return _mapper.Map<AccountClassificationDto>(entity);

        }

    }
}
