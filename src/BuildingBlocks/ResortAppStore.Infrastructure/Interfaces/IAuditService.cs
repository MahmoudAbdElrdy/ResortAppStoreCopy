using Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Common.Interfaces
{
    public interface IAuditService
    {
        T CreateEntity<T>(T entity);
        T UpdateEntity<T>(T entity);
        T DeleteEntity<T>(T entity);

        string UserName { get; }
        string UserId { get; }
        string UserLanguage { get; }
        string Code { get; set; }
        string WebToken { get; }
        string CompanyId { get; }
        string BranchId { get; } 
    }
    public interface IAuditable
    {
        string? CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string? UpdatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
    public interface ISoftDelete
    {
        public State State { get; set; }
    }
    public static class ServiceLocator
    {
        private static IAuditService _auditService;
   
        public static void SetAuditService(IAuditService auditService)
        {
            _auditService = auditService;
            
        }

        public static IAuditService GetAuditService()
        {
            return _auditService;
        }
    }
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _httpContext;
        public AuditService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        //public AuditService()
        //{
        //}

        public T CreateEntity<T>(T entity)
        {
            if (entity is IAuditable model)
            {
                model.CreatedOn = DateTime.UtcNow;
                model.CreatedBy = UserId;
                return (T)model;
            }

            return entity;
        }

        public T UpdateEntity<T>(T entity)
        {
            if (entity is IAuditable model)
            {
                model.UpdatedOn = DateTime.UtcNow;
                model.CreatedBy = UserId;
                //  model.UpdatedBy = _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
                return (T)model;
            }


            return entity;
        }

        public T DeleteEntity<T>(T entity)
        {
            if (entity is ISoftDelete model)
            {
                //model. = DateTime.UtcNow;
                //model.DeletedBy = _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
                model.State = State.Deleted;
                return (T)model;
            }

            return entity;
        }

        bool IsInRole(string role)
        {
            return _httpContext.HttpContext.User.IsInRole(role);
        }

        public string UserName => _httpContext?.HttpContext?.User?.Claims
            .Where(c => c.Type == "userName")
            .SingleOrDefault()?.Value;
        public string UserId => ((_httpContext.HttpContext.User.Claims.Where(c => c.Type == "userLoginId").SingleOrDefault().Value));
        public string BranchId => ((_httpContext.HttpContext.User.Claims.Where(c => c.Type == "branchId").SingleOrDefault().Value));
        public string CompanyId => ((_httpContext.HttpContext.User.Claims.Where(c => c.Type == "companyId").SingleOrDefault().Value));
        public string UserType => ((_httpContext.HttpContext.User.Claims.Where(c => c.Type == "UserType").SingleOrDefault().Value));
        public string RequesterIp => _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();

        //   public string UserLanguage { get { return GetUserLanguageAsync().Result; }  } 
        //  public string UserLanguage => _httpContext.HttpContext.User.Claims
        public string UserLanguage => _httpContext.HttpContext.Request.Headers["Language"].ToString() ?? "ar";
        // public string Code => ((_httpContext.HttpContext.User.Claims.Where(c => c.Type == "Code").SingleOrDefault().Value));
        public string WebToken => _httpContext.HttpContext.Request.Headers["WebToken"].ToString() ?? "Anonymous";

        public string Code { get; set; }

    }
}