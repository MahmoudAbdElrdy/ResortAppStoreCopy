using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Currencies.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Repository
{
   
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<CurrencyTransaction> _context;
        private readonly IGRepository<Currency> _contextCurrency;

        public CurrencyRepository(IGRepository<CurrencyTransaction> userManager, IGRepository<Currency> contextCurrency, IMapper mapper)
        {
            _context = userManager;
            _mapper = mapper;
            _contextCurrency = contextCurrency;

        }
        public async Task<CurrencyTransactionDto> CreateCurrencyTransactionCommand(CreateCurrencyTransactionCommand request)
        {
            var currencyTransaction = _mapper.Map<CurrencyTransaction>(request.InputDto);

            currencyTransaction.CurrencyMaster = null;
            currencyTransaction.CurrencyDetail = null;
            await _context.InsertAsync(currencyTransaction);
            var reverse = new CurrencyTransaction();
            reverse.TransactionFactor = 1 / request.InputDto.TransactionFactor;
            reverse.CurrencyMasterId = request.InputDto.CurrencyDetailId;
            reverse.CurrencyDetailId = request.InputDto.CurrencyMasterId;
            reverse.TransactionDate = request.InputDto.TransactionDate;
            reverse.CurrencyDetail = null;
            reverse.CurrencyMaster = null;
            await _context.InsertAsync(reverse);


            await _context.SaveChangesAsync();

            return _mapper.Map<CurrencyTransactionDto>(currencyTransaction);


        }
        public async Task<CurrencyTransactionDto> DeleteCurrencyTransactionCommand(DeleteCurrencyTransactionCommand request)
        {
            var currencyTransaction = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            var reverse = await _context.FirstOrDefaultAsync(c => c.CurrencyMasterId == currencyTransaction.CurrencyDetailId);

            if (currencyTransaction == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            currencyTransaction.IsDeleted = true;
            reverse.IsDeleted = true;

            await _context.SoftDeleteAsync(currencyTransaction);
            await _context.SoftDeleteAsync(reverse);
            await _context.SaveChangesAsync();
            return _mapper.Map<CurrencyTransactionDto>(currencyTransaction);
        }
        public async Task<int> DeleteListCurrencyTransactionCommand(DeleteListCurrencyTransactionCommand request)
        {

            var currencyTransactionList = await _context.GetAllListAsync(c => request.Ids.Contains(c.Id));

            if (currencyTransactionList == null)
            {
                throw new UserFriendlyException("Not Found");
            }

            foreach (var CurrencyTransaction in currencyTransactionList)
            {
                CurrencyTransaction.IsDeleted = true;
                await _context.SoftDeleteAsync(CurrencyTransaction);
            }


            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<CurrencyTransactionDto> EditCurrencyTransactionCommand(EditCurrencyTransactionCommand request)
        {
            var currencyTransaction = _mapper.Map<CurrencyTransaction>(request.InputDto);

            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.InputDto.Id);

            var reverse = await _context.FirstOrDefaultAsync(c => c.CurrencyMasterId == entityDb.CurrencyDetailId);

            entityDb = _mapper.Map(request.InputDto, entityDb);
            entityDb.CurrencyMaster = null;
            entityDb.CurrencyDetail = null;

            await _context.UpdateAsync(entityDb);


            reverse.TransactionFactor = 1 / request.InputDto.TransactionFactor;
            reverse.TransactionDate = request.InputDto.TransactionDate;
            reverse.CurrencyDetail = null;
            reverse.CurrencyMaster = null;
            await _context.UpdateAsync(reverse);
            await _context.SaveChangesAsync();

            return _mapper.Map<CurrencyTransactionDto>(currencyTransaction);


        }
        public async Task<CurrencyTransactionDto> GetByCurrencyTransactionId(GetByCurrencyTransactionId request)
        {
            var currencyTransaction = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);

            return _mapper.Map<CurrencyTransactionDto>(currencyTransaction);

        }
        public async Task<List<CurrencyTransactionDto>> GetAllCurrenciesTransactionsQueries()
        {
            var currencyTransactionsList = await _context.GetAll().Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<CurrencyTransactionDto>>(currencyTransactionsList);

            return res;

        }
        public async Task<CurrencyDto> GetByCurrencyId(GetByCurrencyId request)
        {
            var currency = await _contextCurrency.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            // var currency = await _context.GetAllIncluding().Include(c => c.CurrenciesMaster).ThenInclude(c => c.CurrencyDetail).Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
            if (currency != null)
            {
                var res = _mapper.Map<CurrencyDto>(currency);
                var CurrenciesDetail = await _context.GetAllIncluding(c => c.CurrencyDetail).Where(c => !c.IsDeleted && c.CurrencyMasterId == request.Id).ToListAsync();
                res.CurrencyTransactionsDto = _mapper.Map<List<CurrencyTransactionDto>>(CurrenciesDetail);
                return res;
            }

            return new CurrencyDto();

        }

        public async Task<List<CurrencyDto>> GetAllCurrencies()
        {
            var currencyList = await _contextCurrency.GetAllIncluding(c => c.CurrenciesDetail).Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<CurrencyDto>>(currencyList);

            return res;
        }
    }
}
