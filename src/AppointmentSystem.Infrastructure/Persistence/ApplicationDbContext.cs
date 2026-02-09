using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Soft delete filter
        builder.Entity<Business>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Branch>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Service>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<BranchService>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Professional>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ProfessionalService>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Client>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Appointment>().HasQueryFilter(x => !x.IsDeleted);

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
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
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
