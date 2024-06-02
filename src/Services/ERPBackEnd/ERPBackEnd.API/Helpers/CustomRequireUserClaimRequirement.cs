using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPBackEnd.API.Helper
{
    public class CustomRequireUserClaimRequirement : IAuthorizationRequirement
    {
        public CustomRequireUserClaimRequirement(string userClaimType, string userClaimValue)
        {
            UserClaimType = userClaimType;
            UserClaimValue = userClaimValue;
        }

        public string UserClaimType { get; }
        public string UserClaimValue { get; }
    }
    public class CustomRequireUserClaim : AuthorizationHandler<CustomRequireUserClaimRequirement>
    {
        public CustomRequireUserClaim()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireUserClaimRequirement requirement)
        {
            var isValid = context.User.Claims.Any(x => x.Type == requirement.UserClaimType && x.Value == requirement.UserClaimValue);
            if (isValid)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
