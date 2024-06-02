using AutoMapper;
using Common;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResortAppStore.Repositories;

namespace Administration.API.Helpers
{
    public class AuthorizeByPermissionsAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly IGRepository<AuthDomain.Entities.Auth.Permission> _permissionRepository;
    
        private readonly IGRepository<AuthDomain.Entities.Auth.User> _userRepository;
        private readonly IGRepository<AuthDomain.Entities.Auth.PermissionRole> _permissionRoleRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public string? _permission { get; }
        public AuthorizeByPermissionsAttribute(
             IGRepository<AuthDomain.Entities.Auth.Permission> permissionRepository,
             IGRepository<AuthDomain.Entities.Auth.User> userRepository,
             IGRepository<AuthDomain.Entities.Auth.PermissionRole> permissionRoleRepository,
             IAuditService auditService


        )
        {
         
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
            _permissionRoleRepository = permissionRoleRepository;
            _auditService = auditService;
        }
        private Result BuildErrorResult(string exception)
        {
            return ResultHelper.CreateFailure(exception);
          
        }
        public override  void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["Controller"].ToString();
            var actionName = context.RouteData.Values["Action"].ToString();
            if (string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
            {
                context.Result = new JsonResult(new
                {
                    Result=false,
                    StatusCode=400,
                    Message="ActionNotFound"

                });
                base.OnActionExecuting(context);
                return;
            }
            if(actionName== "VerifyCodeCommand"|| actionName == "AddPasswordUserCommand" ||
                actionName== "ForgotPasswordUserCommand"||actionName== "GetCustomer" ||
                actionName == "GetUserOrganization" || actionName == "CreateUserOrganization"
                )
            {
                return;
            }
            var user =  _userRepository.GetAllIncluding(c=>c.UserRoles).Where(c => c.UserName == _auditService.UserName&&!c.IsDeleted).FirstOrDefault();
           
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

            if (actionName == "GetLastCode")
                actionName = "Add";
            if (actionName == "GetById")
                actionName = "Edit";
            var roles = user.UserRoles.Select(c => c.RoleId).ToList();
         
            var permissionRole = _permissionRoleRepository.GetAllIncluding(c=>c.Permission.Screen).Where(
                x => roles.Contains(x.RoleId)
                && !x.IsDeleted && !x.Permission.IsDeleted
                &&x.Permission.ActionNameEn==actionName
                &&x.Permission.Screen.ScreenNameEn==controllerName ).FirstOrDefault() ;
            if(permissionRole !=null && permissionRole.IsChecked == false)
            {
                context.HttpContext.Response.StatusCode = 400;

                context.Result = new ObjectResult(BuildErrorResult("NotAuthorized"));

              //  context. = null; //Handled!
            }

            base.OnActionExecuting(context);
        }
        
    }
}
