using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace G64.PedidoAPI.Context
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<T>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Log the error or rethrow it
                    throw;
                }
            }

            return host;
        }
    }
}
