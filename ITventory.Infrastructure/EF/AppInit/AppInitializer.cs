using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITventory.Infrastructure.EF.AppInit
{
    internal sealed class AppInitializer: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AppInitializer(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            var readDbContext = scope.ServiceProvider.GetRequiredService<ReadDbContext>();

            await writeDbContext.Database.MigrateAsync(cancellationToken);
            await readDbContext.Database.MigrateAsync(cancellationToken);

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
