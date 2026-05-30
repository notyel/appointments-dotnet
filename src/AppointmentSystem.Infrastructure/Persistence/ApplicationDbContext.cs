using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Common;
using AppointmentSystem.Application.Interfaces;
namespace AppointmentSystem.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext
{
    private readonly Guid _businessId;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantSettings tenantSettings)
        : base(options)
    {
        _businessId = tenantSettings.BusinessId;
    }

    public DbSet<Business> Businesses => Set<Business>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<BranchService> BranchServices => Set<BranchService>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<ProfessionalService> ProfessionalServices => Set<ProfessionalService>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<BranchSchedule> BranchSchedules => Set<BranchSchedule>();
    public DbSet<ProfessionalSchedule> ProfessionalSchedules => Set<ProfessionalSchedule>();
    public DbSet<BranchHoliday> BranchHolidays => Set<BranchHoliday>();
    public DbSet<ProfessionalAbsence> ProfessionalAbsences => Set<ProfessionalAbsence>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Soft delete filter + tenant isolation — BusinessId directo en cada tabla de negocio
        builder.Entity<Business>().HasQueryFilter(x => !x.IsDeleted && x.Id == _businessId);
        builder.Entity<Branch>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Service>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Professional>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Client>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Appointment>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        builder.Entity<Notification>().HasQueryFilter(x => !x.IsDeleted && x.BusinessId == _businessId);
        // Entidades dependientes: se filtran transitivamente por las raíces superiores
        builder.Entity<BranchService>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ProfessionalService>().HasQueryFilter(x => !x.IsDeleted);

        // Relationships and constraints
        builder.Entity<BranchService>()
            .HasOne(bs => bs.Branch)
            .WithMany(b => b.BranchServices)
            .HasForeignKey(bs => bs.BranchId);

        builder.Entity<BranchService>()
            .HasOne(bs => bs.Service)
            .WithMany(s => s.BranchServices)
            .HasForeignKey(bs => bs.ServiceId);

        builder.Entity<ProfessionalService>()
            .HasOne(ps => ps.Professional)
            .WithMany(p => p.ProfessionalServices)
            .HasForeignKey(ps => ps.ProfessionalId);

        builder.Entity<ProfessionalService>()
            .HasOne(ps => ps.BranchService)
            .WithMany(bs => bs.ProfessionalServices)
            .HasForeignKey(ps => ps.BranchServiceId);

        builder.Entity<Service>()
            .HasOne(s => s.CategoryEntity)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    // Auto-asignar BusinessId en entidades de negocio que lo tengan
                    if (entry.Entity is IHasBusinessId tenantEntity && tenantEntity.BusinessId == Guid.Empty)
                        tenantEntity.BusinessId = _businessId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
