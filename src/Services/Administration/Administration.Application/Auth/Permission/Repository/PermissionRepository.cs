using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.Permission.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Auth.Permissions.Repository 
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IGRepository<PermissionRole> _permissionRoleRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public PermissionRepository(
            IGRepository<Permission> context,
            IGRepository<PermissionRole> permissionRole,
            IMapper mapper, IAuditService auditService)
        {
            _permissionRepository = context;
            _mapper = mapper;
            _auditService = auditService;
            _permissionRoleRepository = permissionRole;
        }
        public async Task<PaginatedList<GetAllPermissionDTO>> GetAllPermissionWithPaginationCommand(GetAllPermissionWithPaginationCommand request)
        {
            List<GetAllPermissionDTO> mappingObjs = new List<GetAllPermissionDTO>();

            var allPermissions = _permissionRepository.GetAllAsNoTracking().Where(c => !c.IsDeleted);

            // var entities = allPermissions.Skip((request.PageIndex - 1) * request.PageSize) .Take(request.PageSize).ToList();
            var entities = allPermissions;

            var totalCount = await allPermissions.CountAsync();

            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => x.RoleId == request.RoleId && !x.IsDeleted).ToList();

            var transferReasonDto = _mapper.Map<List<GetAllPermissionDTO>>(entities);

            if (foundedPermissionRole.Count() > 0)
            {


                foreach (var item in entities)
                {
                    var perm = foundedPermissionRole.FirstOrDefault(c => c.PermissionId == item.Id);

                    if (perm != null)
                    {
                        var obj = new GetAllPermissionDTO()
                        {
                            ControllerNameAr = item?.Screen?.ScreenNameAr,
                            ControllerNameEn = item?.Screen?.ScreenNameEn,
                            // ModuleType = item?.Screen?.ModuleType,
                            Id = item.Id,
                            ActionNameAr = item.ActionNameAr,
                            ActionNameEn = item.ActionNameEn,
                            IsChecked = perm.IsChecked,
                            //Name = item.Name,
                            RoleId = perm.RoleId
                        };
                        mappingObjs.Add(obj);
                    }
                    else
                    {
                        var obj = new GetAllPermissionDTO()
                        {
                            ControllerNameAr = item?.Screen?.ScreenNameAr,
                            ControllerNameEn = item?.Screen?.ScreenNameEn,
                            Id = item.Id,
                            ActionNameAr = item.ActionNameAr,
                            ActionNameEn = item.ActionNameEn,
                            IsChecked = false,
                            // ModuleType = item?.Screen?.ModuleType,
                            ////Name = item.Name,
                            //  RoleId = perm.RoleId
                        };
                        mappingObjs.Add(obj);
                    }


                }

            }
            else
            {
                mappingObjs.AddRange(transferReasonDto);
            }


            return new PaginatedList<GetAllPermissionDTO>(mappingObjs,
                totalCount,
                request.PageIndex,
                request.PageSize);

        }
        public async Task<List<CreatePermissionDto>> CreatePermissionCommand(CreatePermissionCommand request)
        {

            var allPermissions = await _permissionRepository.GetAllListAsync(x => !x.IsDeleted);

            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => x.RoleId == request.RoleId).ToList();

            if (foundedPermissionRole.Count() > 0)
            {
                foreach (var item in foundedPermissionRole)
                {
                    var permission = request.Permissions.Where(x => x.Id == item.PermissionId).FirstOrDefault();

                    item.IsChecked = (bool)permission.IsChecked;
                   
                    await _permissionRoleRepository.UpdateAsync(item);
                  
                }

            
            }


            List<PermissionRole> permissionRoles = new List<PermissionRole>();

         

            await _permissionRoleRepository.SaveChangesAsync();
          
            return request.Permissions;
        }
        public async Task<bool> DeletePermissionCommand(DeletePermissionCommand request)
        {
            var permission = await _permissionRoleRepository.FirstOrDefaultAsync(c => c.PermissionId == request.Id);

            if (permission == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            permission.IsDeleted = true;
          
            await _permissionRoleRepository.SoftDeleteAsync(permission);
            await _permissionRoleRepository.SaveChangesAsync();
            return true;
        }
    }
}
