using AutoMapper;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Units.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Repository
{
   
    public class UnitRepository : IUnitRepository
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<UnitTransaction> _context;
        private readonly IGRepository<Unit> _contextUnit;

        public UnitRepository(IGRepository<UnitTransaction> userManager, IGRepository<Unit> contextUnit, IMapper mapper)
        {
            _context = userManager;
            _mapper = mapper;
            _contextUnit = contextUnit;

        }
        public async Task<UnitTransactionDto> CreateUnitTransaction (CreateUnitTransaction  request)
        {
            var unitTransaction = _mapper.Map<UnitTransaction>(request.InputDto);

            unitTransaction.UnitMaster = null;
            unitTransaction.UnitDetail = null;
            await _context.InsertAsync(unitTransaction);
            var reverse = new UnitTransaction();
            reverse.TransactionFactor = 1 / request.InputDto.TransactionFactor;
            reverse.UnitMasterId = request.InputDto.UnitDetailId;
            reverse.UnitDetailId = request.InputDto.UnitMasterId;
            reverse.UnitDetail = null;
            reverse.UnitMaster = null;
            await _context.InsertAsync(reverse);


            await _context.SaveChangesAsync();

            return _mapper.Map<UnitTransactionDto>(unitTransaction);


        }
        public async Task<UnitTransactionDto> DeleteUnitTransaction (DeleteUnitTransaction  request)
        {
            var unitTransaction = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            var reverse = await _context.FirstOrDefaultAsync(c => c.UnitMasterId == unitTransaction.UnitDetailId);

            if (unitTransaction == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            unitTransaction.IsDeleted = true;
            reverse.IsDeleted = true;

            await _context.SoftDeleteAsync(unitTransaction);
            await _context.SoftDeleteAsync(reverse);
            await _context.SaveChangesAsync();
            return _mapper.Map<UnitTransactionDto>(unitTransaction);
        }
        public async Task<int> DeleteListUnitTransaction (DeleteListUnitTransaction  request)
        {

            var unitTransactionList = await _context.GetAllListAsync(c => request.Ids.Contains(c.Id));

            if (unitTransactionList == null)
            {
                throw new UserFriendlyException("Not Found");
            }

            foreach (var UnitTransaction in unitTransactionList)
            {
                UnitTransaction.IsDeleted = true;
                await _context.SoftDeleteAsync(UnitTransaction);
            }


            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<UnitTransactionDto> EditUnitTransaction(EditUnitTransaction  request)
        {
            var unitTransaction = _mapper.Map<UnitTransaction>(request.InputDto);

            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.InputDto.Id);

            var reverse = await _context.FirstOrDefaultAsync(c => c.UnitMasterId == entityDb.UnitDetailId);

            entityDb = _mapper.Map(request.InputDto, entityDb);
            entityDb.UnitMaster = null;
            entityDb.UnitDetail = null;

            await _context.UpdateAsync(entityDb);


            reverse.TransactionFactor = 1 / request.InputDto.TransactionFactor;
            reverse.UnitDetail = null;
            reverse.UnitMaster = null;
            await _context.UpdateAsync(reverse);
            await _context.SaveChangesAsync();

            return _mapper.Map<UnitTransactionDto>(unitTransaction);


        }
        public async Task<UnitTransactionDto> GetByUnitTransactionId(GetByUnitTransactionId request)
        {
            var unitTransaction = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);

            return _mapper.Map<UnitTransactionDto>(unitTransaction);

        }
        public async Task<List<UnitTransactionDto>> GetAllUnitsTransactionsQueries()
        {
            var unitTransactionsList = await _context.GetAll().Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<UnitTransactionDto>>(unitTransactionsList);

            return res;

        }
        public async Task<UnitDto> GetByUnitId(GetByUnitId request)
        {
            var unit = await _contextUnit.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            // var Unit = await _context.GetAllIncluding().Include(c => c.CurrenciesMaster).ThenInclude(c => c.UnitDetail).Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
            var res = _mapper.Map<UnitDto>(unit);
            var unitsDetail = await _context.GetAllIncluding(c => c.UnitDetail).Where(c => !c.IsDeleted && c.UnitMasterId == request.Id).ToListAsync();
            res.UnitTransactionsDto = _mapper.Map<List<UnitTransactionDto>>(unitsDetail);

            return res;

        }
    }
}
