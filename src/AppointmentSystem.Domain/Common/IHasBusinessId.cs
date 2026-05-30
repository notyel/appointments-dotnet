namespace AppointmentSystem.Domain.Common;

/// <summary>
/// Marca una entidad de negocio que debe estar aislada por tenant.
/// El <see cref="BusinessId"/> identifica a qué <c>Business</c> pertenece el registro
/// y es el mecanismo central del aislamiento multi-tenant.
/// </summary>
public interface IHasBusinessId
{
    Guid BusinessId { get; set; }
}
