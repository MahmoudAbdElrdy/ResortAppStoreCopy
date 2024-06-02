using AutoMapper;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class SalesPersonCommissionRepository : ISalesPersonCommissionRepository
    {

        public SalesPersonCommissionRepository(IMapper mapper,
            IGRepository<SalesPersonCommission> context)
        {

        }
    
    }
}
