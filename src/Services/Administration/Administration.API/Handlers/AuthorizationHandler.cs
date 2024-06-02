using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nashmi.Services.NPay.API.Handlers
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider;
        public AuthorizationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken CancelleationToken)
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {

        //        var securityTokenProvider = scope.ServiceProvider.GetRequiredService<ISecurityTokenProvider>();
        //        var accessToken = securityTokenProvider.GenerateEndToEndSecurityToken();
        //        request.SetBearerToken(accessToken);
        //        return await base.SendAsync(request, CancelleationToken);
        //    }
        //}
    }
}
