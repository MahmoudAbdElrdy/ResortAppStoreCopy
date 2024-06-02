using AutoMapper;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository
{
    public class UserDetailsPackageRepository : IUserDetailsPackageRepository
    {

        private readonly IMapper _mapper;
        private readonly IGRepository<UserDetailsPackage> _userDetailsPackageCtx;
        public UserDetailsPackageRepository(IMapper mapper, IGRepository<UserDetailsPackage> userDetailsPackageCtx)
        {
            _mapper = mapper;
            _userDetailsPackageCtx = userDetailsPackageCtx;
        }

        public async Task<long> CreateUserDetailsPackage(CreateUserDetailsPackageCommand command)
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

            UserDetailsPackage userDetailsPackage = new UserDetailsPackage
            {
                UserId = command.UserId,
                InstrumentPattrenNumber = command.InstrumentPattrenNumber,
             
                NumberOfBranches = command.NumberOfBranches,
                NumberOfCompanies = command.NumberOfCompanies,
                NumberOfUsers = command.NumberOfUsers,
                SubscriptionStartDate = command.SubscriptionStartDate,
                BillPattrenNumber= command.BillPattrenNumber,
                IsActive = true,
                NameAr = command.NameAr,
                NameEn = command.NameEn,
                IsDeleted = false,
                SubscriptionExpiaryDate = expireyDate,
                SubscriptionPrice = command.SubscriptionPrice,
                TypeOfSubscription = (TypeOfSubscription)command.TypeOfSubscription,
            };


            _userDetailsPackageCtx.Insert(userDetailsPackage);
            await _userDetailsPackageCtx.SaveChangesAsync();
            return userDetailsPackage.Id;
        }



        public async Task<List<UserDetailsPackageDto>> GetUserDetailsPackageByUserId(string userId)
        {
            var result = await _userDetailsPackageCtx.GetAll().Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<UserDetailsPackageDto>>(result);
        }
    }
}
