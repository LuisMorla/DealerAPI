using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dealer.WebAPI.Infrastructure.Identity.Context {
    public class IdentityContext : IdentityDbContext<ApplicationUser> {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<ApplicationUser>(e => {
                e.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(e => {
                e.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(e => {
                e.ToTable(name: "UserRoles");
            });


            builder.Entity<IdentityUserLogin<string>>(e => {
                e.ToTable(name: "UserLogin");
            });
        }
    }
}
