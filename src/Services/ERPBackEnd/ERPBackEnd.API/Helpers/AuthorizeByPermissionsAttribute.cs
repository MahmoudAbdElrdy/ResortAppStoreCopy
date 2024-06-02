using AutoMapper;
using Common;
using Common.Interfaces;
using Configuration.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using Newtonsoft.Json;

namespace ERPBackEnd.API.Helpers
{
    public class AuthorizeByPermissionsAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly IGRepository<Permission> _permissionRepository;
        private readonly IGRepository<BranchPermission> _permissionBranchRepository; 
        private readonly IGRepository<User> _userRepository;
        private readonly IUserRepository _userRepos;
        private readonly IGRepository<BillsRolesPermissions> _billsRolesPermissionsRepository;
        private readonly IGRepository<POSBillsRolesPermissions> _posBillsRolesPermissionsRepository;

        private readonly IGRepository<PermissionRole> _permissionRoleRepository;
        private readonly IGRepository<VouchersRolesPermissions> _vouchersRolesPermissionsRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public string? _permission { get; }
        public AuthorizeByPermissionsAttribute(
            IUserRepository userRepos,
             IGRepository<Permission> permissionRepository,
             IGRepository<BillsRolesPermissions> billsRolesPermissionsRepository,
             IGRepository<POSBillsRolesPermissions> posBillsRolesPermissionsRepository,
             IGRepository<User> userRepository,
             IGRepository<PermissionRole> permissionRoleRepository,
             IAuditService auditService,
             IGRepository<VouchersRolesPermissions> vouchersRolesPermissionsRepository,
             IGRepository<BranchPermission>  permissionBranchRepository


        )
        {
            // _permission = permission;
            _permissionRepository = permissionRepository;
            _billsRolesPermissionsRepository = billsRolesPermissionsRepository;
            _posBillsRolesPermissionsRepository = posBillsRolesPermissionsRepository;
            _userRepository = userRepository;
            _permissionRoleRepository = permissionRoleRepository;
            _auditService = auditService;
            _userRepos = userRepos;
            _vouchersRolesPermissionsRepository = vouchersRolesPermissionsRepository;
            _permissionBranchRepository = permissionBranchRepository;
        }
        private Common.Result BuildErrorResult(string exception)
        {
            return ResultHelper.CreateFailure(exception);

        }
        protected string GetCurrentUserRoleId(string userId)
        {
           
            return _userRepos.GetRoleByUserId(userId);

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["Controller"]?.ToString();
            var actionArgument = context.ActionArguments.Where(c => c.Key == "id").FirstOrDefault().Value;
            var actionName = context.RouteData.Values["Action"]?.ToString();

            var x = context.HttpContext.User.Claims.ToList();


            if (string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
            {
                context.Result = new JsonResult(new
                {
                    Result = false,
                    StatusCode = 400,
                    Message = "ActionNotFound"

                });
                base.OnActionExecuting(context);
                return;
            }
            var user = _userRepository.GetAllIncluding(c => c.UserRoles).Where(c => c.UserName == _auditService.UserName && !c.IsDeleted).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            var roleId = GetCurrentUserRoleId(user.Id);
            if ((controllerName == "BillType" || controllerName == "Bill") && actionArgument != null)
            {
                var billsRolesPermissions = _billsRolesPermissionsRepository.GetAll().Where(c => c.RoleId == roleId && c.BillTypeId == (long)actionArgument).FirstOrDefault();
                if (billsRolesPermissions != null)
                {
                    PermissionsJson permissionRole = JsonConvert.DeserializeObject<PermissionsJson>(billsRolesPermissions.PermissionsJson);
                    if (permissionRole == null)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));
                        //  context. = null; //Handled!
                    }
                    else
                    {
                        if (permissionRole.IsShow && actionName == "GetById")
                        {
                            return;
                        }
                        else if (permissionRole.IsAdd && actionName == "Create")
                        {
                            return;
                        }
                        else if (permissionRole.IsDelete && actionName == "Delete")
                        {
                            return;
                        }
                        else if (permissionRole.IsUpdate && actionName == "Update")
                        {
                            return;
                        }
                        else if (permissionRole.IsPrint && actionName == "Print")
                        {
                            return;
                        }
                        else
                        {
                            context.HttpContext.Response.StatusCode = 400;
                            context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));
                        }


                    }


                    //                    base.OnActionExecuting(context);
                }


            }

            else if ((controllerName == "POSBillType" || controllerName == "POSBill") && actionArgument != null)
            {
                var posBillsRolesPermissions = _posBillsRolesPermissionsRepository.GetAll().Where(c => c.RoleId == roleId && c.BillTypeId == (long)actionArgument).FirstOrDefault();
                if (posBillsRolesPermissions != null)
                {
                    PermissionsJson permissionRole = JsonConvert.DeserializeObject<PermissionsJson>(posBillsRolesPermissions.PermissionsJson);
                    if (permissionRole == null)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));
                        //  context. = null; //Handled!
                    }
                    else
                    {
                        if (permissionRole.IsShow && actionName == "GetById")
                        {
                            return;
                        }
                        else if (permissionRole.IsAdd && actionName == "Create")
                        {
                            return;
                        }
                        else if (permissionRole.IsDelete && actionName == "Delete")
                        {
                            return;
                        }
                        else if (permissionRole.IsUpdate && actionName == "Update")
                        {
                            return;
                        }
                        else if (permissionRole.IsPrint && actionName == "Print")
                        {
                            return;
                        }
                        else
                        {
                            context.HttpContext.Response.StatusCode = 400;
                            context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));
                        }


                    }


                }


            }
            else if ((controllerName == "VoucherType" || controllerName == "Voucher") && actionArgument != null)
            {
                var vouchersRolesPermissions = _vouchersRolesPermissionsRepository.GetAll().Where(c => c.RoleId == roleId && c.VoucherTypeId == (long)actionArgument).FirstOrDefault();
                if (vouchersRolesPermissions != null)
                {
                    PermissionsJson permissionRole = JsonConvert.DeserializeObject<PermissionsJson>(vouchersRolesPermissions.PermissionsJson);
                    if (permissionRole == null)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));
                        //  context. = null; //Handled!
                    }
                    else
                    {
                        if (permissionRole.IsShow && actionName == "GetById")
                        {
                            return;
                        }
                        else if (permissionRole.IsAdd && actionName == "Create")
                        {
                            return;
                        }
                        else if (permissionRole.IsDelete && actionName == "Delete")
                        {
                            return;
                        }
                        else if (permissionRole.IsUpdate && actionName == "Update")
                        {
                            return;
                        }
                        else if (permissionRole.IsPrint && actionName == "Print")
                        {
                            return;
                        }
                        else
                        {
                            context.HttpContext.Response.StatusCode = 400;
                            context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

                        }

                    }

                }


            }

            else
            {
                if (actionName == "VerifyCodeCommand" || actionName == "GetDDL" || actionName == "getLeafAccounts" || actionName == "GetCurrenciesTransactions"
                || actionName == "GetLeafAccountsByAccountClassificationId" || actionName == "AddPasswordUserCommand" || actionName == "getByIdTransaction"
                || actionName == "ForgotPasswordUserCommand" || actionName == "GetLastSubscription" || actionName == "GetUnitTransactions" 
                || controllerName=="VoucherType" || controllerName == "BillType" || controllerName == "POSBillType" || controllerName == "Shift"
                || controllerName == "GeneralConfiguration" || actionName == "GetByCurrencyTransactionId"
                || actionName == "GetByUnitTransactionId" || actionName == "getLastCodeByTypeId" 
                || actionName == "close" || actionName == "GetUsers"
                || actionName == "open"
                || actionName == "GetAccountBalalnce" || actionName == "DoPOSBillsCollection" || actionName == "CalculateItemCardBalances" || actionName == "GetItemsCardInRefernces" 
                || actionName == "GetStoresInRefernces"
                || actionName == "getLastCodeByItemGroupId" || actionName == "GetJournalEntryAdditionalById" || actionName == "getNotGenerateEntryBills" || actionName== "GetNotificationConfigurations" || actionName== "GetStoresInDocument"
                || actionName == "GetAccountBalalnce" || actionName == "CalculateItemCardBalances"
                || actionName == "UploadFile" || actionName == "UploadFileAsBinary" || actionName== "UploadFileWihPath"
                || actionName == "getLastCodeByItemGroupId" || actionName == "GetJournalEntryAdditionalById" || actionName == "getNotGenerateEntryBills" || actionName== "GetNotificationConfigurations"
                || actionName == "GetBillPayments" || actionName== "GetVouchersBillPays" || actionName == "GetBillPaid" || actionName == "GetInstallmentPaid"
                || actionName == "GetNotPostToWarehousesAutomaticallyBills" || actionName == "GetNotGeneratedEntryBills" || actionName == "Print" || actionName == "getReportList" || actionName ==  "GetAllCardList"
                || actionName == "GetUnsyncedElectronicBills" || actionName== "GetTablesByFloorId" || actionName == "GetReservedTablesByFloorId" 
                )
                {
                    return;
                }

                if (user == null)
                {
                    context.Result = new JsonResult(new
                    {
                        Result = false,
                        StatusCode = 400,
                        Message = "UserNotFound"

                    });
                    base.OnActionExecuting(context);
                    return;
                }

                if (actionName == "GetLastCode" || actionName == "Create")
                    actionName = "Add";
                if (actionName == "GetById" || actionName == "Update")
                    actionName = "Edit";
                if (actionName == "GetAll" || actionName == "ShowTree")
                    actionName = "Show";
                if (actionName == "SoftDeleteList" || actionName == "deleteReport")
                    actionName = "Delete";
                if (actionName == "getReportList")
                    actionName = "Print";
                if (actionName == "cancelDefaultReport")
                    actionName = "Cancel";
                if (actionName == "setDefaultReport")
                    actionName = "Default";
                if (actionName == "Collect")
                    actionName = "Collect";
                if (actionName == "Reject")
                    actionName = "Reject";
                if (actionName == "Post")
                    actionName = "Post";

                var roles = user.UserRoles.Select(c => c.RoleId).ToList();

                var permissionRole = _permissionRoleRepository.GetAllIncluding(c => c.Permission.Screen).Where(
                    x => roles.Contains(x.RoleId)
                    && !x.IsDeleted && !x.Permission.IsDeleted
                    && x.Permission.ActionName == actionName
                    && x.Permission.Screen.Name == controllerName).FirstOrDefault();

                var permissionBranch = _permissionBranchRepository.GetAllIncluding(c => c.Branch).
                    Where(
                      c => c.BranchId == Convert.ToInt64(_auditService.BranchId)
                      && c.UserId == _auditService.UserId
                      && !c.IsDeleted
                      && c.ActionName == actionName).FirstOrDefault();

                //if (permissionBranch == null)
                //{
                //    context.HttpContext.Response.StatusCode = 400;
                //    context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

                //}
                //else
                if (permissionBranch != null && permissionBranch.IsChecked == false)
                {
                    context.HttpContext.Response.StatusCode = 400;

                    context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

                }
                if (permissionRole == null)
                {
                    context.HttpContext.Response.StatusCode = 400;
                    context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

                }
                else if (permissionRole != null && permissionRole.IsChecked == false)
                {
                    context.HttpContext.Response.StatusCode = 400;

                    context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

                }


                base.OnActionExecuting(context);
            }


           
        }



    }
    public class PermissionsJson
    {
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsShow { get; set; }
        public bool IsPrint { get; set; }
    }

}
