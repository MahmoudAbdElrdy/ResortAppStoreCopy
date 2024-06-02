using AutoMapper;
using Common.Extensions;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Features.Customers.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Repository
{
    public class PromoCodeRespository : GMappRepository<PromoCodes, PromoCodeDto, long> , IPromoCodeRespository
    {
        private readonly IGRepository<PromoCodes> _context;
        private readonly IMapper _mapper;

        public PromoCodeRespository(IGRepository<PromoCodes> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _context = mainRepos;
            _mapper = mapper;
        }

        public async Task<PromoCodeDto> CreatePromoCodeCommand(PromoCodeDto request)
        {
            if(request != null)
            {
                var promoCode = _mapper.Map<PromoCodes>(request);
                promoCode.CreatedAt = DateTime.Now;
                promoCode.CreatedBy = "admin";
                promoCode.IsDeleted = false;
                await _context.InsertAsync(promoCode);
                await _context.SaveChangesAsync();

                return _mapper.Map<PromoCodeDto>(promoCode);

            }
            return new PromoCodeDto();
        }

        public async Task<int> DeletePromoCodeCommand(long id)
        {
            var promoCode = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (promoCode != null)
            {
                await _context.DeleteAsync(promoCode);
                int result = await _context.SaveChangesAsync();
                return result;

            }
            return -1;
        }

        public async Task<PromoCodeDto> EditPromoCodeCommand(PromoCodeDto request)
        {
            var entityDb = await _context.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (entityDb == null)
                throw new UserFriendlyException("Not Found");

            entityDb = _mapper.Map(request, entityDb);
            await _context.UpdateAsync(entityDb);
            await _context.SaveChangesAsync();
            return _mapper.Map<PromoCodeDto>(entityDb);
        }

        public async Task<List<PromoCodeDto>> GetAllPromoCodesCommand()
        {

            var promoCodeList = await _context.GetAll().Where(x => !x.IsDeleted).ToListAsync();
            var res = _mapper.Map<List<PromoCodeDto>>(promoCodeList);

            return res;
        }

        public async Task<PromoCodeDto> GetPromoCodeCommandbyId(long id)
        {
            var promoCode = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (promoCode != null)
            {
                var res = _mapper.Map<PromoCodeDto>(promoCode);
                return res;
            }

            return new PromoCodeDto();
        }

        public async Task<PromoCodeDto> GetPromoCodeCommandbyCode(string id)
        {
            PromoCodeDto promoCodeDto = new PromoCodeDto();
            var promoCode = await _context.FirstOrDefaultAsync(x => x.PromoCode == id 
            && x.IsDeleted != true && x.IsActive == true);
            if(promoCode != null)
            {
                if(promoCode.ToDate < DateTime.Now)
                {
                    throw new UserFriendlyException("This PromoCode is Expired");
                }
                else
                {
                    promoCodeDto = _mapper.Map<PromoCodeDto>(promoCode);
                }

            }
            return promoCodeDto;
        }
    }
}
