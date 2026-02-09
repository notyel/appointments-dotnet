# Documentación del Sistema de Gestión de Citas

## Arquitectura
El sistema sigue los principios de **Clean Architecture**:
- **Domain**: Entidades de negocio, enums e interfaces de repositorio.
- **Application**: DTOs, servicios de aplicación y lógica de negocio.
- **Infrastructure**: Implementación de persistencia (EF Core), Identidad y servicios externos.
- **WebUI**: Interfaz de usuario basada en ASP.NET MVC, CSS propio y Vanilla JavaScript.

## Flujos Críticos

### 1. Reserva de Citas (Booking Flow)
1. El cliente selecciona una sucursal.
2. Selecciona un servicio de la sucursal.
3. Selecciona un profesional asignado a ese servicio.
4. Selecciona un horario disponible (validado contra la agenda del profesional y la capacidad de la sucursal).
5. Completa sus datos y confirma la reserva.
6. El sistema valida nuevamente la disponibilidad antes de persistir y registra una notificación.

### 2. Cancelación de Citas
- El cliente puede buscar sus citas por correo electrónico.
- Solo se permite la cancelación con al menos **24 horas de antelación**.
- El estado de la cita se actualiza a `Cancelled` (soft-delete preservado para auditoría).

### 3. Administración
- **Admin General**: Acceso total a todas las sucursales.
- **Admin de Sucursal**: Acceso restringido únicamente a la sucursal asignada (aislamiento de datos mediante claims y verificaciones en controlador).
- **Dashboard**: Vista diaria de citas filtrada por sucursal.

## Seguridad y Roles
- **Roles**: `Admin`, `BranchAdmin`, `Receptionist`, `Specialist`.
- **Autenticación**: ASP.NET Core Identity.
- **Autorización**: Atributos `[Authorize(Roles = "...")]` y verificaciones manuales para aislamiento multi-sucursal.

## Puntos de Extensión
- **Pagos**: El `BookingController.Create` puede redirigir a una pasarela de pago antes de confirmar la cita.
- **Notificaciones Reales**: Implementar `INotificationService` para integrar con SendGrid (Email) o Twilio (WhatsApp).
- **Facturación**: Integrar en el flujo de `Attended` (marcado por el especialista) para generar comprobantes.

## Restricciones Técnicas Cumplidas
- ✅ 100% .NET 8.
- ✅ Sin librerías externas de CSS (Bootstrap, etc.).
- ✅ Sin frameworks JS (jQuery, React, etc.).
- ✅ JavaScript Vanilla y CSS propio.
