using AutoMapper;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository
{
    public class UserDetailsModuleRepository : IUserDetailsModuleRepository
    {

        private readonly IMapper _mapper;
        private readonly IGRepository<UserDetailsModule> _userDetailsModuleCtx;
        public UserDetailsModuleRepository(IMapper mapper, IGRepository<UserDetailsModule> userDetailsModuleCtx)
        {
            _mapper = mapper;
            _userDetailsModuleCtx = userDetailsModuleCtx;
        }

        public async Task<long> CreateUserDetailsModules(CreateUserDetailsModulesCommand command)
        {
            if (string.IsNullOrEmpty(command.NameAr) || string.IsNullOrWhiteSpace(command.NameAr))
                throw new UserFriendlyException("Name Ar Required");

            if (string.IsNullOrEmpty(command.NameEn) || string.IsNullOrWhiteSpace(command.NameEn))
                throw new UserFriendlyException("Name En Required");

            if (string.IsNullOrEmpty(command.UserId) || string.IsNullOrWhiteSpace(command.UserId))
                throw new UserFriendlyException("User is Required");

            DateTime? expireyDate = null;

            if (command.TypeOfSubscription == (int)TypeOfSubscription.MonthlySubscription)
                expireyDate = command.SubscriptionStartDate.AddMonths(1);

            if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlyQuarterSubscription)
                expireyDate = command.SubscriptionStartDate.AddMonths(3);

            if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlyHalfSubscription)
                expireyDate = command.SubscriptionStartDate.AddMonths(6);

            if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlySubscription)
                expireyDate = command.SubscriptionStartDate.AddYears(1);

            UserDetailsModule userDetailsModule = new UserDetailsModule
            {
                UserId = command.UserId,
                BillPattrenPrice = command.BillPattrenPrice,
                SubscriptionStartDate = command.SubscriptionStartDate,
                InstrumentPattrenPrice = command.InstrumentPattrenPrice,
                IsActive = true,
                IsFree = command.IsFree,
                NameAr = command.NameAr,
                NameEn = command.NameEn,
                IsDeleted = false,
                OtherModuleId = command.OtherModuleId,
                OtherUserFullBuyingSubscriptionPrice = command.OtherUserFullBuyingSubscriptionPrice,
                OtherUserMonthlySubscriptionPrice = command.OtherUserMonthlySubscriptionPrice,
                OtherUserYearlySubscriptionPrice = command.OtherUserYearlySubscriptionPrice,
                SubscriptionExpiaryDate = expireyDate,
                SubscriptionPrice = command.SubscriptionPrice,
                TypeOfSubscription = (TypeOfSubscription)command.TypeOfSubscription,
                BillPattrenNumber = command.BillPattrenNumber,
                InstrumentPattrenNumber = command.InstrumentPattrenNumber,
                NumberOfBranches = command.NumberOfBranches,
                NumberOfCompanies= command.NumberOfCompanies,
                NumberOfUser= command.NumberOfUser,
                
                        
            };
            _userDetailsModuleCtx.Insert(userDetailsModule);
            await _userDetailsModuleCtx.SaveChangesAsync();
            return userDetailsModule.Id;
        }

        public async Task<List<UserDetailsModuleDto>> GetUserDetailsModulesByUserId(string userId)
        {
            var result = await _userDetailsModuleCtx.GetAll().Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<UserDetailsModuleDto>>(result);
        }
    }
}
