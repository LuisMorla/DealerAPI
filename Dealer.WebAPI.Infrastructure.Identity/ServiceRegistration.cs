using Dealer.WebAPI.Infrastructure.Identity.Context;
using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dealer.WebAPI.Infrastructure.Identity {
    public static class ServiceRegistration {
        public static void AddIdentityInfrastructure(this IServiceCollection service, IConfiguration configuration) {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
                service.AddDbContext<IdentityContext>(o => o.UseInMemoryDatabase("IdentityDB"));
            } else {
                service.AddDbContext<IdentityContext>(o => {
                    o.EnableSensitiveDataLogging();
                    o.UseSqlServer(configuration.GetConnectionString("IdentityConnetion"), m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity
            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            service.AddAuthentication();
            #endregion
        }
    }
}
