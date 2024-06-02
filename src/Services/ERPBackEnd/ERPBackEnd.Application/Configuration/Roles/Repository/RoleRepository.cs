using AutoMapper;
using Common;
using Common.Constants;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Services.Service;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Roles.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Roles.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Services;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Repository
{
    public class RoleRepository:IRoleRepository
    {
        private readonly IGRepository<Role> _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IGRepository<BillsRolesPermissions> _billsRolesPermissions;
        private readonly IGRepository<POSBillsRolesPermissions> _posBillsRolesPermissions;
        private readonly IGRepository<VouchersRolesPermissions> _vouchersRolesPermissions;
        private readonly IGRepository<Screen> _contextScreen;
        private readonly IGRepository<PermissionRole> _permissionRoleRepository;
        private readonly DeleteService _deleteService;
        private readonly IPermissionService _permissionService;
        public RoleRepository(
            IMapper mapper,
            IGRepository<Role> context,
            IGRepository<Permission> permissionRepository,
            IGRepository<BillsRolesPermissions> billsRolesPermissions,
            IGRepository<POSBillsRolesPermissions> posBillsRolesPermissions,

            IGRepository<VouchersRolesPermissions> vouchersRolesPermissions,
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
            _billsRolesPermissions = billsRolesPermissions;
            _posBillsRolesPermissions = posBillsRolesPermissions;
            _vouchersRolesPermissions = vouchersRolesPermissions;
            _contextScreen = contextScreen;
            _permissionRoleRepository = permissionRoleRepository;
            _deleteService = deleteService;
            _permissionService = permissionService;
        }
        public async Task<RoleDto> CreateRoleCommand(CreateRoleCommand request)
        {
            if (request.Code == _auditService.Code)
            {
                var lastCode = await _context.GetAll().OrderByDescending(c => c.Code).Where(x => !x.IsDeleted).FirstOrDefaultAsync();

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
                NormalizedName = request.NameEn.ToUpper(),
                CreatedBy = _auditService.UserId,
                Code = request.Code,
                     };

          

            List<PermissionRole> permissionRoles = new List<PermissionRole>();

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
           
            await _context.InsertAsync(role);
            await _context.SaveChangesAsync();

           var saveSubRoleResult = await CreateSubRoles(request, role.Id);

            if(saveSubRoleResult.Success)
            {
                var superAdminRole = await _context.FirstOrDefaultAsync(c => c.NameAr == "SuperAdmin" && !c.IsDeleted);
                //Seeding super Admin Roles
                await CreateSubRoles(request, superAdminRole.Id);
            }
      
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<ResponseResult> CreateSubRoles(CreateRoleCommand request,string roleId)
        {
            try
            {
                ResponseResult responseResult = new ResponseResult();
                if (request.BillsRolesPermissions.Count() > 0)
                {
                    List<BillsRolesPermissions> billsRolesPermissions = new List<BillsRolesPermissions>();
                    foreach (var dbPermission in request.BillsRolesPermissions)
                    {
                        billsRolesPermissions.Add(new BillsRolesPermissions()
                        {
                            IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                            RoleId = roleId,
                            PermissionsJson = dbPermission.PermissionsJson,
                            BillTypeId = dbPermission.BillTypeId,



                        });
                    }
                    await _billsRolesPermissions.InsertAsync(billsRolesPermissions);

                }
                if (request.POSBillsRolesPermissions.Count() > 0)
                {
                    List<POSBillsRolesPermissions> posBillsRolesPermissions = new List<POSBillsRolesPermissions>();
                    foreach (var dbPermission in request.POSBillsRolesPermissions)
                    {
                        posBillsRolesPermissions.Add(new POSBillsRolesPermissions()
                        {
                            IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                            RoleId = roleId,
                            PermissionsJson = dbPermission.PermissionsJson,
                            BillTypeId = dbPermission.BillTypeId,



                        });
                    }
                    await _posBillsRolesPermissions.InsertAsync(posBillsRolesPermissions);

                }
                if (request.VouchersRolesPermissions.Count() > 0)
                {
                    List<VouchersRolesPermissions> voucherRolesPermissions = new List<VouchersRolesPermissions>();
                    foreach (var dbPermission in request.VouchersRolesPermissions)
                    {
                        voucherRolesPermissions.Add(new VouchersRolesPermissions()
                        {
                            IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                            RoleId = roleId,
                            PermissionsJson = dbPermission.PermissionsJson,
                            VoucherTypeId = dbPermission.VoucherTypeId,



                        });
                    }
                    await _vouchersRolesPermissions.InsertAsync(voucherRolesPermissions);

                }
             
                responseResult = new ResponseResult
                {
                    Message = "Success",
                    Success = true,
                };
                return responseResult;
            }
            catch (Exception)
            {

                throw;
            }


       
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
            role.IsActive = request.IsActive;
        
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

            var billsRolesPermissions = _billsRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();
            var posBillsRolesPermissions = _posBillsRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();

            var voucherRolesPermissions = _vouchersRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();
            if (billsRolesPermissions.Count() > 0)
            {
                foreach (var permission in billsRolesPermissions)
                {

                    await _billsRolesPermissions.DeleteAsync(permission);

                }
            }
            if (posBillsRolesPermissions.Count() > 0)
            {
                foreach (var permission in posBillsRolesPermissions)
                {

                    await _posBillsRolesPermissions.DeleteAsync(permission);

                }
            }
            if (voucherRolesPermissions.Count() > 0)
            {
                foreach (var permission in voucherRolesPermissions)
                {

                    await _vouchersRolesPermissions.DeleteAsync(permission);

                }
            }
            await _context.SaveChangesAsync();
            if (request.BillsRolesPermissions.Count() > 0)
            {
                List<BillsRolesPermissions> billsRolesPermissionsList = new List<BillsRolesPermissions>();
                foreach (var dbPermission in request.BillsRolesPermissions)
                {
                    billsRolesPermissionsList.Add(new BillsRolesPermissions()
                    {
                        IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                        RoleId = role.Id,
                        PermissionsJson = dbPermission.PermissionsJson,
                        BillTypeId = dbPermission.BillTypeId,



                    });
                }
                await _billsRolesPermissions.InsertAsync(billsRolesPermissionsList);

            }
            if (request.POSBillsRolesPermissions.Count() > 0)
            {
                List<POSBillsRolesPermissions> posBillsRolesPermissionsList = new List<POSBillsRolesPermissions>();
                foreach (var dbPermission in request.POSBillsRolesPermissions)
                {
                    posBillsRolesPermissionsList.Add(new POSBillsRolesPermissions()
                    {
                        IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                        RoleId = role.Id,
                        PermissionsJson = dbPermission.PermissionsJson,
                        BillTypeId = dbPermission.BillTypeId,



                    });
                }
                await _posBillsRolesPermissions.InsertAsync(posBillsRolesPermissionsList);

            }
            if (request.VouchersRolesPermissions.Count() > 0)
            {
                List<VouchersRolesPermissions> voucherRolesPermissionsList = new List<VouchersRolesPermissions>();
                foreach (var dbPermission in request.VouchersRolesPermissions)
                {
                    voucherRolesPermissionsList.Add(new VouchersRolesPermissions()
                    {
                        IsUserChecked = dbPermission.IsUserChecked.HasValue ? dbPermission.IsUserChecked : false,
                        RoleId = role.Id,
                        PermissionsJson = dbPermission.PermissionsJson,
                        VoucherTypeId = dbPermission.VoucherTypeId,



                    });
                }
                await _vouchersRolesPermissions.InsertAsync(voucherRolesPermissionsList);

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
                if (role.Id == "1")
                {
                    throw new UserFriendlyException("can't-delete-record");
                }
                var excluded = new List<string>() { "AspNetRoleClaims", "PermissionRoles", "BillsRolesPermissions", "VouchersRolesPermissions" };
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
            if (request.Id == "1")
            {
                throw new UserFriendlyException("can't-delete-record");
            }
            var role = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (role == null)
            {
                throw new UserFriendlyException("Not Found");
            }
     
            var excluded = new List<string>() { "AspNetRoleClaims", "PermissionRoles", "BillsRolesPermissions", "VouchersRolesPermissions" };
            var isDeleted = await _deleteService.ScriptCheckDeleteExcluded("AspNetRoles", "Id", role.Id, excluded);
            if (!isDeleted)
                throw new UserFriendlyException("can't-delete-record");
            var foundedPermissionRole = _permissionRoleRepository.GetAllList(x => x.RoleId == request.Id).ToList();

            foreach (var permission in foundedPermissionRole)
            {
                permission.IsDeleted = true;
                await _permissionRoleRepository.SoftDeleteAsync(permission);
            }
            var billsRolesPermissions = _billsRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();
            var posBillsRolesPermissions = _posBillsRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();

            var voucherRolesPermissions = _vouchersRolesPermissions.GetAllList(x => x.RoleId == request.Id).ToList();
            if (billsRolesPermissions.Count() > 0)
            {
                foreach (var permission in billsRolesPermissions)
                {

                    await _billsRolesPermissions.DeleteAsync(permission);

                }
            }

            if (posBillsRolesPermissions.Count() > 0)
            {
                foreach (var permission in posBillsRolesPermissions)
                {

                    await _posBillsRolesPermissions.DeleteAsync(permission);

                }
            }

            if (voucherRolesPermissions.Count() > 0)
            {
                foreach (var permission in voucherRolesPermissions)
                {

                    await _vouchersRolesPermissions.DeleteAsync(permission);

                }
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
            var billsRolesPermissions = await _billsRolesPermissions.GetAllListAsync(x => x.RoleId == request.Id);
            var posBillsRolesPermissions = await _posBillsRolesPermissions.GetAllListAsync(x => x.RoleId == request.Id);
            var voucherRolesPermissions = await _vouchersRolesPermissions.GetAllListAsync(x => x.RoleId == request.Id);
            var role = await _context.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var screens = await _contextScreen.GetAllListAsync(x => !x.IsDeleted);
            var requestPermission = new GetAllPermissionWithPaginationCommand();
            requestPermission.RoleId = request.Id;
            var permissions = await _permissionService.GetAllPermissions(requestPermission);
            var result = new RoleDto();
            result = _mapper.Map<RoleDto>(role);
            result.Permissions = permissions;
            result.VouchersRolesPermissions = _mapper.Map<List<VouchersRolesPermissionsDto>>(voucherRolesPermissions); 
            result.BillsRolesPermissions = _mapper.Map<List<BillsRolesPermissionsDto>>(billsRolesPermissions);
            result.POSBillsRolesPermissions = _mapper.Map<List<POSBillsRolesPermissionsDto>>(posBillsRolesPermissions);

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
