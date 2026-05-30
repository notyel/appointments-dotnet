using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Entities;

public class Service : BaseEntity, IHasBusinessId
{
    /// <summary>Tenant al que pertenece este servicio.</summary>
    public Guid BusinessId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Clasificación de negocio del servicio. Se almacena directamente como columna
    /// en esta tabla. Para la relación persistida con entidad propia, ver <see cref="CategoryId"/> y <see cref="CategoryEntity"/>.
    /// </summary>
    public BusinessCategory BusinessCategory { get; set; }

    public string? ImageUrl { get; set; }

    /// <summary>Indica si el servicio es destacado/popular en la sucursal.</summary>
    public bool IsPopular { get; set; } = false;

    /// <summary>FK opcional hacia la entidad <see cref="Category"/>
    public Guid? CategoryId { get; set; }
    public Category? CategoryEntity { get; set; }

    public ICollection<BranchService> BranchServices { get; set; } = new List<BranchService>();
}

public class BranchService : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();
}
