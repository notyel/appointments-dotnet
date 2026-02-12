# Solución al Problema de Migración ImageUrl

## Problema Identificado

La migración para agregar `ImageUrl` a la tabla `Branches` no se aplicó correctamente porque:

1. Se creó manualmente sin el archivo `.Designer.cs` requerido por Entity Framework Core
2. El `ModelSnapshot` no fue actualizado
3. Entity Framework no reconoce la migración manual

## Soluciones Disponibles

### Opción 1: Script SQL Manual (Solución Rápida)

Si la aplicación está corriendo y no puedes detenerla:

1. Conéctate a tu base de datos PostgreSQL
2. Ejecuta el script SQL ubicado en:
   ```
   AppointmentSystem.Infrastructure/SQL/add_imageurl_to_branches.sql
   ```

Esto agregará la columna inmediatamente sin necesidad de detener la aplicación.

### Opción 2: Crear Migración Correcta con EF Core (Solución Recomendada)

**Pasos a seguir:**

1. **Detén la aplicación** en Visual Studio (Shift + F5)

2. **Verifica que el modelo está actualizado:**
   - Asegúrate que `Branch.cs` tenga la propiedad `ImageUrl`
   - Asegúrate que `BranchDto` tenga la propiedad `ImageUrl`

3. **Crea la migración correcta:**
   ```bash
   dotnet ef migrations add AddImageUrlToBranch --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
   ```

4. **Aplica la migración:**
   ```bash
   dotnet ef database update --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
   ```

5. **Verifica que se aplicó:**
   ```bash
   dotnet ef migrations list --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
   ```

6. **Reinicia la aplicación**

### Opción 3: Actualizar Manualmente el ModelSnapshot (Avanzado)

Si ya ejecutaste el script SQL y solo necesitas que EF Core reconozca el cambio:

1. Edita `ApplicationDbContextModelSnapshot.cs`
2. Agrega la propiedad `ImageUrl` en la sección de Branch (después de Email, antes de IsActive):

```csharp
b.Property<string>("ImageUrl")
    .IsRequired()
    .HasColumnType("text");
```

3. Crea una migración vacía para marcar como aplicada:
   ```bash
   dotnet ef migrations add AddImageUrlToBranch_Manual --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
   ```

4. Edita la migración generada y deja los métodos Up y Down vacíos (porque ya aplicaste el cambio con SQL)

## Verificación Final

Después de aplicar cualquiera de las soluciones, verifica:

1. **En la base de datos:**
   ```sql
   SELECT column_name, data_type 
   FROM information_schema.columns 
   WHERE table_name = 'Branches' AND column_name = 'ImageUrl';
   ```

2. **En la aplicación:**
   - La vista `Index.cshtml` debe mostrar las imágenes de las sucursales
   - No debe haber errores al cargar la página de inicio

## Estado Actual de Migraciones

Migraciones aplicadas:
- `20260211171102_InitialCreate`
- `20260211184043_AddImageUrlToServicesAndProfessionals`

Migración pendiente:
- `AddImageUrlToBranch` (necesita ser creada correctamente)

## Notas Importantes

- **SIEMPRE detén la aplicación antes de crear migraciones** con `dotnet ef`
- Los archivos de migración incluyen dos archivos:
  - `[Timestamp]_[Name].cs` - La migración en sí
  - `[Timestamp]_[Name].Designer.cs` - Metadata para EF Core
- El archivo `ModelSnapshot.cs` debe reflejar el estado actual del modelo
