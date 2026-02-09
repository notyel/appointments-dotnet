using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Enums;
using AppointmentSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppointmentSystem.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        logger.LogInformation("Checking if seeding is needed...");
        if (await context.Branches.AnyAsync())
        {
            logger.LogInformation("Database already seeded.");
            return;
        }

        logger.LogInformation("Seeding database...");

        var businessId = Guid.NewGuid();
        var business = new Business
        {
            Id = businessId,
            Name = "Centro de Estética Belleza Total",
            Description = "Los mejores servicios de estética y SPA."
        };
        context.Businesses.Add(business);

        var branch1Id = Guid.NewGuid();
        var branch1 = new Branch
        {
            Id = branch1Id,
            BusinessId = businessId,
            Name = "Sucursal Central",
            Address = "Av. Principal 123",
            Phone = "555-0101",
            Email = "central@bellezatotal.com",
            SimultaneousCapacity = 2
        };

        var branch2Id = Guid.NewGuid();
        var branch2 = new Branch
        {
            Id = branch2Id,
            BusinessId = businessId,
            Name = "Sucursal Norte",
            Address = "Calle Norte 456",
            Phone = "555-0202",
            Email = "norte@bellezatotal.com",
            SimultaneousCapacity = 1
        };
        context.Branches.AddRange(branch1, branch2);

        var s1Id = Guid.NewGuid();
        var service1 = new Service { Id = s1Id, Name = "Corte de Cabello", Description = "Corte profesional", Category = ServiceCategory.Hairdressing };
        var s2Id = Guid.NewGuid();
        var service2 = new Service { Id = s2Id, Name = "Manicura", Description = "Cuidado de uñas", Category = ServiceCategory.Aesthetics };
        var s3Id = Guid.NewGuid();
        var service3 = new Service { Id = s3Id, Name = "Masaje Relajante", Description = "60 min de relax", Category = ServiceCategory.SPA };
        context.Services.AddRange(service1, service2, service3);

        var bs1Id = Guid.NewGuid();
        var bs1 = new BranchService { Id = bs1Id, BranchId = branch1Id, ServiceId = s1Id, DurationMinutes = 30, Price = 25 };
        var bs2Id = Guid.NewGuid();
        var bs2 = new BranchService { Id = bs2Id, BranchId = branch1Id, ServiceId = s2Id, DurationMinutes = 45, Price = 35 };
        var bs3Id = Guid.NewGuid();
        var bs3 = new BranchService { Id = bs3Id, BranchId = branch1Id, ServiceId = s3Id, DurationMinutes = 60, Price = 60 };
        var bs4Id = Guid.NewGuid();
        var bs4 = new BranchService { Id = bs4Id, BranchId = branch2Id, ServiceId = s1Id, DurationMinutes = 30, Price = 30 };
        context.BranchServices.AddRange(bs1, bs2, bs3, bs4);

        var p1Id = Guid.NewGuid();
        var prof1 = new Professional { Id = p1Id, FirstName = "Ana", LastName = "García", Email = "ana@bellezatotal.com", Phone = "111", BranchId = branch1Id };
        var p2Id = Guid.NewGuid();
        var prof2 = new Professional { Id = p2Id, FirstName = "Carlos", LastName = "Pérez", Email = "carlos@bellezatotal.com", Phone = "222", BranchId = branch1Id };
        context.Professionals.AddRange(prof1, prof2);

        context.ProfessionalServices.Add(new ProfessionalService { ProfessionalId = p1Id, BranchServiceId = bs1Id });
        context.ProfessionalServices.Add(new ProfessionalService { ProfessionalId = p1Id, BranchServiceId = bs2Id });
        context.ProfessionalServices.Add(new ProfessionalService { ProfessionalId = p2Id, BranchServiceId = bs3Id });

        await context.SaveChangesAsync();

        // Seed Roles
        string[] roles = { "Admin", "BranchAdmin", "Receptionist", "Specialist" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminEmail = "admin@bellezatotal.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // Seed Branch Admin
        var branchAdminEmail = "sucursal@bellezatotal.com";
        var branchAdminUser = await userManager.FindByEmailAsync(branchAdminEmail);
        if (branchAdminUser == null)
        {
            branchAdminUser = new IdentityUser { UserName = branchAdminEmail, Email = branchAdminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(branchAdminUser, "Branch123!");
            await userManager.AddToRoleAsync(branchAdminUser, "BranchAdmin");
            // Add BranchId claim
            await userManager.AddClaimAsync(branchAdminUser, new System.Security.Claims.Claim("BranchId", branch1Id.ToString()));
        }
    }
}
