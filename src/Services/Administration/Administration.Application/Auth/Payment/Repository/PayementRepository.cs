using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Payment.Repository
{
    public class PayementRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IGRepository<UserPayment> _userPayment;
        public PayementRepository(
             IConfiguration configuration,
             IGRepository<UserPayment> userPayment,
             IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userPayment = userPayment;
        }





    }
}
