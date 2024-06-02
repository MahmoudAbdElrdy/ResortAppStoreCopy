using Google;
using ResortAppStore.Services.Administration.Infrastructure.Persistence;

namespace ResortAppStore.Services.Administration.API.Helpers
{
    public class SessionCleanupService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public SessionCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CleanUpSessions, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private async void CleanUpSessions(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();
                var expiredSessions = dbContext.UserTokens
                    .Where(s => s.Expiry < DateTime.Now || !s.IsValid)
                    .ToList();

                dbContext.UserTokens.RemoveRange(expiredSessions);
                await dbContext.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
