using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.Permission.Dto;
using ResortAppStore.Services.Administration.Application.Auth.Roles.Dto;
using ResortAppStore.Services.Administration.Application.Roles.Dto;
using ResortAppStore.Services.Administration.Application.Services;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Roles.Repository
{
    public class RoleRepository:IRoleRepository
    {
        private readonly IGRepository<Role> _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IGRepository<Screen> _contextScreen;
        private readonly IGRepository<PermissionRole> _permissionRoleRepository;
        private readonly DeleteService _deleteService;
        private readonly IPermissionService _permissionService;
        public RoleRepository(
            IMapper mapper,
            IGRepository<Role> context,
            IGRepository<Permission> permissionRepository,
            IGRepository<Screen> contextScreen,
             IGRepository<PermissionRole>  permissionRoleRepository,
             DeleteService deleteService,
             IPermissionService permissionService,
            IAuditService auditService)
        {
            _context = context;
            _auditService = auditService;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _contextScreen = contextScreen;
            _permissionRoleRepository = permissionRoleRepository;
            _deleteService = deleteService;
            _permissionService = permissionService;
        }
        public async Task<RoleDto> CreateRoleCommand(CreateRoleCommand request)
        {
            if (request.Code == _auditService.Code)
            {
                var lastCode = await _context.GetAll().OrderByDescending(c => c.Id).Where(x => !x.IsDeleted).FirstOrDefaultAsync();

                var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));

                if (request.Code != code)
                {
                    var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);

                    request.Code = code;
                }
            }
            else
            {
                var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);
                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }


            var role = new Role()
            {
                NameAr = request.NameAr,
                Name = request.NameEn,
                CreatedBy = _auditService.UserId,
                Code = request.Code,
                NormalizedName = request.NameEn.ToUpper(),
                // Id=request.Code

            };

            //  await _context.InsertAsync(role);

            List<PermissionRole> permissionRoles = new List<PermissionRole>();

            //var allPermissions = await _permissionRepository.GetAllListAsync(x => !x.IsDeleted);

            foreach (var dbPermission in request.Permissions)
            {
                permissionRoles.Add(new PermissionRole()
                {
                    IsChecked = dbPermission.IsChecked.HasValue ? true : false,
                    RoleId = role.Id,
                    PermissionId = dbPermission.Id,
                    CreatedBy = _auditService.UserId,
                    CreatedAt = DateTime.UtcNow

                });
            }
            role.PermissionRoles = permissionRoles;
            //   await _permissionRoleRepository.InsertRangeAsync(permissionRoles);
            await _context.InsertAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleDto>(role);
        }
        public async Task<RoleDto> EditRoleCommand(EditRoleCommand request)
        {
            var role = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (role == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Id != role.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");
            role.NameAr = request.NameAr;
            role.Name = request.NameEn;
            role.NormalizedName = request.NameEn.ToUpper();

            var allPermissions = await _permissionRepository.GetAllListAsync(x => !x.IsDeleted);

            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => x.RoleId == request.Id).ToList();

            var permissionIds = foundedPermissionRole.Select(c => c.PermissionId).ToList();

            var permissionRoleIds = allPermissions.Select(c => c.Id).ToList();

            var foundListUpdate = foundedPermissionRole.Where(c => permissionRoleIds.Contains(c.PermissionId));

            var foundListInsert = allPermissions.Where(c => !permissionIds.Contains(c.Id));



            foreach (var item in foundListUpdate)
            {
                var permission = request.Permissions.Where(x => x.Id == item.PermissionId).FirstOrDefault();

                item.IsChecked = (bool)permission.IsChecked;
                item.UpdateBy = _auditService.UserId;
                item.UpdatedAt = System.DateTime.UtcNow;
                await _permissionRoleRepository.UpdateAsync(item);

            }
            List<PermissionRole> permissionRoles = new List<PermissionRole>();
            if (foundListInsert.Count() > 0)
            {
                foreach (var item in foundListInsert)
                {
                    var permission = request.Permissions.Where(x => x.Id == item.Id).FirstOrDefault();


                    permissionRoles.Add(new PermissionRole()
                    {
                        IsChecked = permission.IsChecked.HasValue ? true : false,
                        RoleId = request.Id,
                        PermissionId = permission.Id,
                        IsDeleted = false,
                        CreatedAt = System.DateTime.UtcNow,
                        CreatedBy = _auditService.UserId

                    });

                }

                await _permissionRoleRepository.InsertRangeAsync(permissionRoles);

            }

            //if (request.Permissions != null && request.Permissions.Count() > 0)
            //{
            //    foreach (var dbPermission in allPermissions)
            //    {

            //        var permission = request.Permissions.Where(x => x.Id == dbPermission.Id).FirstOrDefault();

            //        permissionRoles.Add(new PermissionRole()
            //        {
            //            IsChecked = permission != null ? true : false,
            //            RoleId = request.RoleId,
            //            PermissionId = dbPermission.Id,
            //            IsDeleted = false,

            //        });
            //    }

            //    await _permissionRoleRepository.InsertRangeAsync(permissionRoles);

            //}


            await _context.UpdateAsync(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }
        public async Task<int> DeleteListRoleCommand(DeleteListRoleCommand request)
        {

            var roleList = await _context.GetAllAsNoTracking().Where(c => request.Ids.Contains(c.Id) && !c.IsDeleted).ToListAsync();

            if (roleList == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var deleteListCheck = true;

            foreach (var role in roleList)
            {
                var excluded = new List<string>() { "AspNetRoleClaims", "PermissionRoles" };
                var isDeleted = await _deleteService.ScriptCheckDeleteExcluded("AspNetRoles", "Id", role.Id, excluded);
                if (!isDeleted)
                {
                    deleteListCheck = false;
                }
                else
                {
                    role.IsDeleted = true;
                    await _context.SoftDeleteAsync(role);
                }

            }
            if (!deleteListCheck)
                throw new UserFriendlyException("can't-delete-some-records");

            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => request.Ids.Contains(x.RoleId)).ToList();

            foreach (var permission in foundedPermissionRole)
            {
                permission.IsDeleted = true;
                await _permissionRoleRepository.SoftDeleteAsync(permission);
            }

            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<RoleDto> DeleteRoleCommand(DeleteRoleCommand request) 
        {
            var role = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (role == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var excluded = new List<string>() { "AspNetRoleClaims", "PermissionRoles" };
            var isDeleted = await _deleteService.ScriptCheckDeleteExcluded("AspNetRoles", "Id", role.Id, excluded);
            if (!isDeleted)
                throw new UserFriendlyException("can't-delete-record");
            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => x.RoleId == request.Id).ToList();

            foreach (var permission in foundedPermissionRole)
            {
                permission.IsDeleted = true;
                await _permissionRoleRepository.SoftDeleteAsync(permission);
            }
            role.IsDeleted = true;

            await _context.SoftDeleteAsync(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }
        public async Task<List<RoleDto>> GetAllRolesQuery()
        {
            var roleList = await _context.GetAllListAsync(x => !x.IsDeleted);

            return _mapper.Map<List<RoleDto>>(roleList);

        }
        public async Task<PaginatedList<RoleDto>> GetAllRolesWithPaginationCommand(GetAllRolesWithPaginationCommand request) 
        {
            var query = _context.GetAllAsNoTracking().Where(c => !c.IsDeleted);

            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.NameAr.Contains(request.Filter));
            }

            var entities = query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize).ToList();

            var totalCount = await query.CountAsync();

            var transferReasonDto = _mapper.Map<List<RoleDto>>(entities);

            return new PaginatedList<RoleDto>(transferReasonDto,
                totalCount,
                request.PageIndex,
                request.PageSize);

        }
        public async Task<RoleDto> GetByRoleId(GetByRoleId request) 
        {
            var role = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var screens = await _contextScreen.GetAllListAsync(x => !x.IsDeleted);
            var requestPermission = new GetAllPermissionWithPaginationCommand();
            requestPermission.RoleId = request.Id;
            var permissions = await _permissionService.GetAllPermissions(requestPermission);
            var result = new RoleDto();
            result = _mapper.Map<RoleDto>(role);
            result.Permissions = permissions;
            result.Screens = _mapper.Map<List<ScreenDto>>(screens);
            foreach (var screen in result?.Screens)
            {
                screen.Permissions = permissions?.Items?.Where(c => c.ScreenId == screen.Id).ToList();
            }
            return result;

        }
        public async Task<PermissionDtoCodeRole> GetLastCode() 
        {
            var lastCode = await _context.GetAll().OrderByDescending(c => c.CreatedAt).Where(x => !x.IsDeleted).FirstOrDefaultAsync();

            var screens = await _contextScreen.GetAllListAsync(x => !x.IsDeleted);

            var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));

            _auditService.Code = code;

            var allPermissions = await _permissionRepository.GetAllListAsync(x => !x.IsDeleted);

            var reslut = new PermissionDtoCodeRole();

            reslut.Code = code;

            reslut.Permissions = _mapper.Map<List<GetAllPermissionDTO>>(allPermissions);

            reslut.Screens = _mapper.Map<List<ScreenDto>>(screens);
            foreach (var screen in reslut?.Screens)
            {
                screen.Permissions = reslut.Permissions?.Where(c => c.ScreenId == screen.Id).ToList();
            }
            return reslut;

        }

    }
}
