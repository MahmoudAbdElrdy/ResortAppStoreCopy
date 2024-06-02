using Google;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Services.Administration.Infrastructure.Persistence;

namespace ResortAppStore.Services.Administration.API.Handlers
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
  {
            var dbContext = context.RequestServices.GetRequiredService<IdentityServiceDbContext>();
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            var path = context.Request.Path.Value.ToLower();

            //var excludedPaths = new[] { "/api/technicalSupport/login", "/api/technicalsupport/checksession" };

            //if (excludedPaths.Contains(path))
            //{
            //    await _next(context);
            //    return;
            //}
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var userSession = await dbContext.UserTokens.FirstOrDefaultAsync(t => t.Token == token);
                if (userSession == null || !userSession.IsValid)
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("Invalid Token");
                    return;
                }

                // Update last activity
                userSession.LastActivity = DateTime.Now;
                userSession.IsValid = true;
                dbContext.UserTokens.Update(userSession);
                await dbContext.SaveChangesAsync();
            }

            await _next(context);
        }
    }

    public static class TokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenValidationMiddleware>();
        }
    }

}
