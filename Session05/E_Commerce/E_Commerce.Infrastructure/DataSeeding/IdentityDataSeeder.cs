using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Infrastructure.DataSeeding;

internal class IdentityDataSeeder : IDataSeeder
{
    private readonly StoreIdentityDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<IdentityDataSeeder> _logger;

    public IdentityDataSeeder(StoreIdentityDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ILogger<IdentityDataSeeder> logger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedDataAsync(CancellationToken ct = default)
    {
        try
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(ct);

            if (!await _roleManager.Roles.AnyAsync(ct))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!await _userManager.Users.AnyAsync(ct))
            {
                var admin = new ApplicationUser()
                {
                    DisplayName = "Aliaa Tarek",
                    Email = "aliaa@gmail.com",
                    UserName = "AliaaTarek",
                    PhoneNumber = "01265478536"
                };

                var createResult = await _userManager.CreateAsync(admin, "P@ssw0rd");
                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "SuperAdmin");
                }
                else
                {
                    var errors = string.Join(';', createResult.Errors.Select(e => e.Description));
                    _logger.LogWarning($"Can Not Seed Default Admin {errors}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Identity Data Seeding Failed.");
            return;
        }
    }
}