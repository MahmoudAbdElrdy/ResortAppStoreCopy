using AutoMapper;
using Common.Extensions;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Subscription.CashPayment.Dto;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription
{
    public class CashPaymentInfoRepository : GMappRepository<CashPaymentInfo, CashPaymentInfoDto, long>, ICashPaymentInfoRepository
    {
        private readonly IGRepository<CashPaymentInfo> _context;
        private readonly IMapper _mapper;

        public CashPaymentInfoRepository(IGRepository<CashPaymentInfo> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
        }

        public async Task<CashPaymentInfoDto> EditCashPaymentInfo(CashPaymentInfoDto request)
        {
            var entityDb = await _context.GetAll().AsNoTracking().FirstOrDefaultAsync();

            if (entityDb == null)
            {
                entityDb = new CashPaymentInfo()
                {
                    CompanyAddress = request.CompanyAddress,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    MobileNumber = request.MobileNumber,
                    CreatedAt = System.DateTime.Now
                };
                await _context.InsertAsync(entityDb);
                await _context.SaveChangesAsync();
                return request;
            }
            else
            {
                entityDb.CompanyAddress = request.CompanyAddress;
                entityDb.PhoneNumber = request.PhoneNumber;
                entityDb.Email = request.Email;
                entityDb.MobileNumber = request.MobileNumber;
                entityDb.UpdatedAt = System.DateTime.Now;
                await _context.UpdateAsync(entityDb);
                await _context.SaveChangesAsync();
                return request;
            }


        
           
        
        }

        public async Task<List<CashPaymentInfoDto>> GetAllCashPaymentInfo()
        {
            var CashPaymentInfoList = await _context.GetAll().Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<CashPaymentInfoDto>>(CashPaymentInfoList);

            return res;
        }

        public async Task<CashPaymentInfoDto> GetCashPaymentInfo()
        {
            var CashPaymentInfo = await _context.GetAll().FirstOrDefaultAsync();

            if (CashPaymentInfo != null)
            {
                var res = _mapper.Map<CashPaymentInfoDto>(CashPaymentInfo);
                return res;
            }

            return new CashPaymentInfoDto();
        }
    }
}
