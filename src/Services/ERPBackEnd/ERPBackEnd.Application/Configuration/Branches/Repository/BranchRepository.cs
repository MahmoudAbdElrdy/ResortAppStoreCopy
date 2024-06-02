using AutoMapper;
using Common;
using Common.Extensions;
using Common.Infrastructures;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Branches.Repository
{
    public class BranchRepository : GMappRepository<Branch, BranchDto, long>, IBranchRepository
    {
        IGRepository<BranchPermission> _gBranchPermission { get; set; }
        public BranchRepository(IGRepository<Branch> mainRepos, IGRepository<BranchPermission> gBranchPermission, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _gBranchPermission = gBranchPermission;
        }

        public Task<PaginatedList<BranchDto>> GetAllIncluding(Paging paging)
        {

            return base.GetAllIncluding(paging, x => x.Company);
        }
        public async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "Branches", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "Branches", "Id");
        }
        //public async Task<BranchDto> Add(BranchDto input)
        //{
          


        //    return await base.Create(input);
        //}

        public async Task<List<BranchPermissionDto>> AllBranchPermission(List<object> branchIds)
        {
            var list = await _gBranchPermission.GetAllIncluding(c => c.Branch).Where(c => branchIds.Contains(c.BranchId) && !c.IsDeleted).ToListAsync();
            var result = new List<BranchPermissionDto>();
            foreach (var permission in list)
            {
                result.Add(new BranchPermissionDto()
                {
                    Id = permission.Id,
                    IsChecked = permission.IsChecked,
                    BranchId = permission.BranchId.Value,
                    ActionName = permission.ActionName,
                    ActionNameEn = permission.ActionNameEn,
                    ActionNameAr = permission.ActionNameAr,
                    BranchNameEn = permission?.Branch?.NameEn,
                    BranchNameAr = permission?.Branch?.NameAr,
                });
            }
            return result;
        }
        public async Task<List<BranchPermissionListDto>> AllBranchPermissionList(List<object> branchIds,string userId)
        {
            var isSave = false;
            var list = await _gBranchPermission
            .GetAllIncluding(c => c.Branch)
               .WhereIf(!string.IsNullOrWhiteSpace(userId)&&userId!="0", c=>c.UserId == userId)
               .Where(c => branchIds.Contains(c.BranchId) && !c.IsDeleted)
               .ToListAsync();
            var insertIfEmpty = list.Select(c => c.BranchId).ToList();
            
            foreach (var input in branchIds) 
            {
                if (!insertIfEmpty.Any(c => c == Convert.ToInt64(input)))
                {
                    var entities = new[]
          {
                  new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId =Convert.ToInt64(input),
                    ActionName = "Show",
                    ActionNameEn = "Show",
                    ActionNameAr = "عرض",
                    UserId=userId

                },
                   new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                     BranchId =Convert.ToInt64(input),
                    ActionName = "Add",
                    ActionNameEn = "Add",
                    ActionNameAr = "اضافة",
                     UserId=userId
                },
                    new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                     BranchId =Convert.ToInt64(input),
                    ActionName = "Edit",
                    ActionNameEn = "Edit",
                    ActionNameAr = "تعديل",
                     UserId=userId
                },
                     new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                     BranchId =Convert.ToInt64(input),
                    ActionName = "Delete",
                    ActionNameEn = "Delete",
                    ActionNameAr = "حذف",
                     UserId=userId
                }
                };
                    await _gBranchPermission.InsertRangeAsync(entities);
                    isSave = true;
                }

                
               
            }
            if (isSave)
            {
                await _gBranchPermission.SaveChangesAsync();
            }
           

            var result = new List<BranchPermissionListDto>();
            list = await _gBranchPermission
            .GetAllIncluding(c => c.Branch)
               .WhereIf(!string.IsNullOrWhiteSpace(userId) && userId != "0", c => c.UserId == userId)
               .Where(c => branchIds.Contains(c.BranchId) && !c.IsDeleted)
               .ToListAsync();
            foreach (var permission in list)
            {
                var branchPermissions = new BranchPermissionDto
                {
                    Id = permission.Id,
                    IsChecked = permission.IsChecked,
                    BranchId = permission.BranchId.Value,
                    ActionName = permission.ActionName,
                    ActionNameEn = permission.ActionNameEn,
                    ActionNameAr = permission.ActionNameAr,
                    BranchNameEn = permission?.Branch?.NameEn,
                    BranchNameAr = permission?.Branch?.NameAr,
                    UserId=userId
                    // Populate other properties of BranchPermissionDto as needed
                };

                var branchPermissionListDto = result.FirstOrDefault(dto => dto.BranchId == permission.BranchId.Value);

                if (branchPermissionListDto == null)
                {
                    branchPermissionListDto = new BranchPermissionListDto
                    {
                        BranchId = permission.BranchId.Value,
                        BranchNameEn = permission?.Branch?.NameEn,
                        BranchNameAr = permission?.Branch?.NameAr,
                        branchPermissions = new List<BranchPermissionDto>(),
                    };
                    result.Add(branchPermissionListDto);
                }

                branchPermissionListDto.branchPermissions.Add(branchPermissions);
            }

            return result;
        }

        public async Task<bool> EditBranchPermission(List<BranchPermissionDto> branchPermissions)
        {

            foreach (var permission in branchPermissions)
            {
                var permissionDb = await _gBranchPermission.FirstOrDefaultAsync(permission.Id);
                permissionDb.IsChecked = permission.IsChecked;
                await _gBranchPermission.UpdateAsync(permissionDb);
            }
            await _gBranchPermission.SaveChangesAsync();

            return true;
        }
    }
}
