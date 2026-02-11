# AppointmentSystem - Guía de Desarrollo

Sistema web de gestión de citas desarrollado con ASP.NET Core 8 y PostgreSQL.

## ?? Tabla de Contenidos

- [Estructura del Proyecto](#estructura-del-proyecto)
- [Requisitos Previos](#requisitos-previos)
- [Configuración Inicial](#configuración-inicial)
- [Migraciones de Entity Framework Core](#migraciones-de-entity-framework-core)
- [Comandos Útiles](#comandos-útiles)

## ??? Estructura del Proyecto

```
src/
??? AppointmentSystem.Domain/          # Entidades del dominio
??? AppointmentSystem.Application/     # Lógica de aplicación
??? AppointmentSystem.Infrastructure/  # Acceso a datos y persistencia
??? AppointmentSystem.WebUI/          # Capa de presentación (MVC)
```

## ?? Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework Core Tools](https://docs.microsoft.com/ef/core/cli/dotnet)

### Instalar EF Core Tools

```bash
dotnet tool install --global dotnet-ef
```

### Verificar instalación

```bash
dotnet ef --version
```

## ?? Configuración Inicial

1. **Clonar el repositorio**

```bash
git clone https://github.com/notyel/appointments-dotnet.git
cd appointments-dotnet/src
```

2. **Configurar la cadena de conexión**

Editar `appsettings.json` en el proyecto `AppointmentSystem.WebUI`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=appointmentdb;Username=postgres;Password=tu_password"
  }
}
```

3. **Restaurar paquetes**

```bash
dotnet restore
```

4. **Aplicar migraciones**

```bash
dotnet ef database update --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

5. **Ejecutar la aplicación**

```bash
cd AppointmentSystem.WebUI
dotnet run
```

## ??? Migraciones de Entity Framework Core

### Crear una nueva migración

Cuando realices cambios en las entidades del dominio, crea una migración:

```bash
dotnet ef migrations add NombreDeLaMigracion --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

**Ejemplo:**
```bash
dotnet ef migrations add AddPhoneNumberToClient --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Aplicar migraciones a la base de datos

#### Opción 1: Aplicar manualmente

```bash
dotnet ef database update --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

#### Opción 2: Aplicar a una migración específica

```bash
dotnet ef database update NombreDeLaMigracion --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

#### Opción 3: Automático al ejecutar la aplicación

La aplicación está configurada para aplicar migraciones automáticamente al iniciar mediante:

```csharp
context.Database.Migrate();
```

en `Program.cs`.

### Revertir una migración

Para deshacer la última migración (solo si no se ha aplicado a la BD):

```bash
dotnet ef migrations remove --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Revertir a una migración anterior (en la BD)

```bash
dotnet ef database update MigracionAnterior --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

Para revertir todas las migraciones:

```bash
dotnet ef database update 0 --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Listar migraciones

Ver todas las migraciones del proyecto:

```bash
dotnet ef migrations list --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Generar script SQL de migraciones

Generar script SQL sin aplicarlo a la BD:

```bash
dotnet ef migrations script --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI --output migration.sql
```

Generar script desde una migración específica:

```bash
dotnet ef migrations script MigracionInicial MigracionFinal --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Eliminar la base de datos

?? **PRECAUCIÓN: Esto eliminará todos los datos**

```bash
dotnet ef database drop --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

Con confirmación forzada:

```bash
dotnet ef database drop --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI --force
```

## ??? Comandos Útiles

### Compilar la solución

```bash
dotnet build
```

### Ejecutar tests

```bash
dotnet test
```

### Limpiar artefactos de compilación

```bash
dotnet clean
```

### Restaurar paquetes NuGet

```bash
dotnet restore
```

### Ver información del DbContext

```bash
dotnet ef dbcontext info --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Scaffold del DbContext (ingeniería inversa)

Si necesitas generar entidades desde una BD existente:

```bash
dotnet ef dbcontext scaffold "Host=localhost;Database=appointmentdb;Username=postgres;Password=password" Npgsql.EntityFrameworkCore.PostgreSQL --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI --output-dir Entities --context-dir Persistence --context ApplicationDbContext --force
```

## ?? Notas Importantes

### ? Manejo de fechas y horas (DateTime)

**El sistema usa UTC para todas las fechas en la base de datos:**

```csharp
// ? CORRECTO - Siempre especifica UTC
var dateUtc = DateTime.SpecifyKind(date, DateTimeKind.Utc);

// ? CORRECTO - Usa UtcNow
var now = DateTime.UtcNow;

// ? INCORRECTO - Evita DateTime.Now
var now = DateTime.Now;

// ? CORRECTO - Consultas con rangos
var appointments = await repository.FindAsync(a => 
    a.StartTime >= startUtc && 
    a.StartTime < endUtc);

// ? INCORRECTO - No uses .Date en consultas a PostgreSQL
var appointments = await repository.FindAsync(a => 
    a.StartTime.Date == date.Date);
```

**Conversión de zonas horarias:**

El modelo `Branch` incluye el campo `TimeZone` para manejar diferentes zonas horarias:

```csharp
// Convertir UTC a zona horaria local de la sucursal
var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(branch.TimeZone);
var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);

// Convertir zona horaria local a UTC
var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, timeZoneInfo);
```

### Convenciones para nombres de migraciones

- Usa PascalCase: `AddNewFeature`
- Sé descriptivo: `AddEmailToUser` en lugar de `Update1`
- Usa verbos de acción: `Add`, `Update`, `Remove`, `Create`, etc.

### Ejemplo de nombres de migraciones:

? **Buenos ejemplos:**
- `AddAppointmentStatusEnum`
- `CreateProfessionalScheduleTable`
- `UpdateBranchPhoneNumberLength`
- `RemoveObsoleteClientFields`

? **Malos ejemplos:**
- `Migration1`
- `Changes`
- `Fix`
- `Update`

### Comandos abreviados (desde la carpeta src)

Para simplificar, puedes crear alias o scripts. Ejemplo en PowerShell:

```powershell
# Archivo: migrate.ps1
param([string]$name)
dotnet ef migrations add $name --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

Uso:
```powershell
.\migrate.ps1 AddNewFeature
```

### Variables de entorno para la cadena de conexión

Para mayor seguridad, usa variables de entorno:

```bash
# Windows (PowerShell)
$env:ConnectionStrings__DefaultConnection="Host=localhost;Database=appointmentdb;Username=postgres;Password=tu_password"

# Linux/Mac
export ConnectionStrings__DefaultConnection="Host=localhost;Database=appointmentdb;Username=postgres;Password=tu_password"
```

## ?? Solución de Problemas

### Error: "No DbContext was found"

Asegúrate de estar en la carpeta `src` y especificar correctamente los proyectos:

```bash
dotnet ef migrations add NombreMigracion --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

### Error: "Build failed"

Compila primero el proyecto:

```bash
dotnet build
```

### Error: "Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone'"

Este error ocurre cuando intentas guardar un DateTime con `Kind=Local` en PostgreSQL.

**? Solución implementada en el proyecto:**

El sistema está configurado para trabajar correctamente con UTC:

1. **Todas las fechas se guardan en UTC** en la base de datos
2. **Conversión automática a UTC** en los servicios antes de guardar
3. **Uso de rangos de fechas** en lugar de `.Date` para consultas eficientes

```csharp
// ? CORRECTO - Especificar UTC
var dateUtc = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);

// ? CORRECTO - Usar rangos de fechas
var appointments = await repository.FindAsync(a => 
    a.StartTime >= dateUtc && 
    a.StartTime < dateUtc.AddDays(1));

// ? INCORRECTO - Usar .Date en consultas
var appointments = await repository.FindAsync(a => 
    a.StartTime.Date == date.Date);
```

**?? Manejo de zonas horarias:**

El sistema incluye el campo `TimeZone` en la entidad `Branch` para manejar diferentes zonas horarias por sucursal.

```csharp
// Cada sucursal puede tener su propia zona horaria
public class Branch
{
    public string TimeZone { get; set; } = "UTC";
    // ...
}
```

**?? Mejores prácticas implementadas:**

- ? Guardar siempre en UTC en la base de datos
- ? Convertir a zona horaria local solo en la presentación
- ? Usar `DateTime.UtcNow` en lugar de `DateTime.Now`
- ? Evitar `EnableLegacyTimestampBehavior` en sistemas nuevos
- ? Usar rangos de fechas en lugar de `.Date` para mejor rendimiento

**? NO uses el switch legacy:**

```csharp
// ? NO RECOMENDADO para sistemas nuevos
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

Este switch oculta problemas reales y no es compatible a futuro.

### Error de conexión a PostgreSQL

Verifica que:
1. PostgreSQL esté ejecutándose
2. Las credenciales en `appsettings.json` sean correctas
3. El puerto 5432 esté disponible

```bash
# Verificar estado de PostgreSQL (Windows)
Get-Service postgresql*

# Verificar estado de PostgreSQL (Linux)
sudo systemctl status postgresql
```

## ?? Recursos Adicionales

- [Documentación de EF Core](https://docs.microsoft.com/ef/core/)
- [Migraciones en EF Core](https://docs.microsoft.com/ef/core/managing-schemas/migrations/)
- [Npgsql - PostgreSQL para .NET](https://www.npgsql.org/efcore/)
- [ASP.NET Core Identity](https://docs.microsoft.com/aspnet/core/security/authentication/identity)

---

**Desarrollado con ?? usando ASP.NET Core 8 y PostgreSQL**
