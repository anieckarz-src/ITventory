using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITventory.Infrastructure.Identity
{
    public static class IdentityMigrationsExtensions
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using UserManagerDbContext context = scope.ServiceProvider.GetRequiredService<UserManagerDbContext>();

            context.Database.Migrate();
        }
    }
}
