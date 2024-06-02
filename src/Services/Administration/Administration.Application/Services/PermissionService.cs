using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Services
{
    public class PermissionService: IPermissionService
    {
        private readonly IGRepository<AuthDomain.Entities.Auth.Permission> _permissionRepository;
        private readonly IGRepository<AuthDomain.Entities.Auth.PermissionRole> _permissionRoleRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public PermissionService(
            IGRepository<AuthDomain.Entities.Auth.Permission> context,
            IGRepository<AuthDomain.Entities.Auth.PermissionRole> permissionRole,
            IMapper mapper, IAuditService auditService)
        {
            _permissionRepository = context;
            _mapper = mapper;
            _auditService = auditService;
            _permissionRoleRepository = permissionRole;
        }
        public async Task<PaginatedList<GetAllPermissionDTO>> GetAllPermissions(GetAllPermissionWithPaginationCommand request)
        {
             List<GetAllPermissionDTO> mappingObjs = new List<GetAllPermissionDTO>();

            var allPermissions = _permissionRepository.GetAllAsNoTracking().Include(c => c.Screen).Where(c => !c.IsDeleted);

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
                            Id = item.Id,
                            ActionNameAr = item.ActionNameAr,
                            ActionNameEn = item.ActionNameEn,
                            IsChecked = perm.IsChecked,
                            //Name = item.Name,
                            ScreenId=item.ScreenId,
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
                            ScreenId = item.ScreenId,
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

    }
}
