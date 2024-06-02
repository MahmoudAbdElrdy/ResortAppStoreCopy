using AutoMapper;
using Common.Infrastructures;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Repository
{
    public class CustomerCardRepository : GMappRepository<CustomerCard, CustomerCardDto, long> , ICustomerCardRepository
    {
        IMapper mapper;
        IGRepository<CustomerCard> mainRepos;
        public CustomerCardRepository(IMapper mapper,
        IGRepository<CustomerCard> mainRepos , DeleteService deleteService) :base(mainRepos , mapper , deleteService) {
        
            this.mapper = mapper;
            this.mainRepos = mainRepos;
        }
        public async Task<PaginatedList<CustomerCardDto>>  GetAllCardList(Paging paging)
        {
           
            var customerCardList = await mainRepos.GetAllIncluding(c => c.Account).Where(x=>x.IsDeleted!=true).ToListAsync();
            var customerCardDtoList = mapper.Map<List<CustomerCardDto>>(customerCardList);

            return new PaginatedList<CustomerCardDto>(customerCardDtoList, customerCardList.Count,paging.PageIndex, paging.PageSize);

        }
    }
}
