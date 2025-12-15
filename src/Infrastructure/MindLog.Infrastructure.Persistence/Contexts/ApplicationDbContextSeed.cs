using Microsoft.AspNetCore.Identity;
using MindLog.Application.Common.Models;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Contexts;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAdminUserAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        const string adminRoleName = ApplicationRoles.Admin;

        // بررسی وجود نقش
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = adminRoleName,
                NormalizedName = adminRoleName.ToUpper(),
                Description = "System Administrator",
                CreatedAt = DateTimeOffset.UtcNow,
            };

            await roleManager.CreateAsync(role);
        }

        // اطلاعات یوزر ادمین
        var adminEmail = "admin@system.com";
        var adminUserName = "admin";
        var adminPassword = "Admin@12345";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (!result.Succeeded)
            {
                throw new Exception("خطا در ایجاد یوزر ادمین: " +
                    string.Join(" | ", result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}
