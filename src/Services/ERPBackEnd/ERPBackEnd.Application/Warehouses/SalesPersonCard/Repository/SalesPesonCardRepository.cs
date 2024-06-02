using AutoMapper;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class SalesPesonCardRepository : ISalesPersonCardRepository
    {

        public SalesPesonCardRepository(IMapper mapper,
            IGRepository<SalesPersonCard> context)
        {

        }

    }
}
