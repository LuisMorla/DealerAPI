using Dealer.WebAPI.Extensions;
using Dealer.WebAPI.Infrastructure.Identity;
using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Dealer.WebAPI.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    try {

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultClientUser.ClientAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SuperAdminAsync(userManager, roleManager);
    } catch {
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseHealthChecks("/health");
app.UseSession();

app.MapControllers();

app.Run();
