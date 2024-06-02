using AutoMapper;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Dto;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Repository
{
    public class SupplierCardRepository : ISupplierCardRepository
    {

        public SupplierCardRepository(IMapper mapper,
            IGRepository<SupplierCard> context)
        {
            
        }

       
    }
}
