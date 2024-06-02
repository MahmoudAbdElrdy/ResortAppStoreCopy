using AutoMapper;
using Common.Infrastructures;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Repository
{
    public interface ICompanyRepository
    {
        Task<PaginatedList<CompanyDto>> GetAllIncluding(Paging paging);
    }
    public class CompanyRepository : GMappRepository<Company, CompanyDto, long>, ICompanyRepository
    {
        IGRepository<Company> _mainRepos { get; set; } 
        IMapper _mapper { get; set; }
        public CompanyRepository(IGRepository<Company> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _mainRepos = mainRepos;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CompanyDto>> GetAllIncluding(Paging paging)
        {

            var entities =  _mainRepos.GetAllIncluding(c => c.Currency).Include(c => c.Country).AsNoTracking().Where(c=>!c.IsDeleted);
            var entitiesQuery = await entities.ToListAsync();
            var totalCount = await entities.CountAsync();

            var transferReasonDto = _mapper.Map<List<CompanyDto>>(entities);
            foreach(var item in transferReasonDto)
            {
                item.Symbol = entities?.Select(c => c.Currency)?.FirstOrDefault()?.Symbol;
            }

            return new PaginatedList<CompanyDto>(transferReasonDto,
              totalCount,
              paging.PageIndex,
              paging.PageSize);
        }
    }
}
