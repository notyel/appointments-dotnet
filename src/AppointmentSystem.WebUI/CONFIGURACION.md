# Configuración del Sistema de Citas

## Configuración del Negocio

La información del negocio se configura en el archivo `appsettings.json` bajo la sección `AppConfig`:

```json
"AppConfig": {
  "BusinessName": "Sistema de Citas",
  "Contact": {
    "Phone": "+1 (555) 123-4567",
    "Email": "contacto@sistemacitas.com",
    "Address": "123 Calle Principal, Ciudad, País"
  }
}
```

### Cómo Modificar la Configuración

1. Abre el archivo `AppointmentSystem.WebUI/appsettings.json`
2. Localiza la sección `AppConfig`
3. Modifica los valores según tu negocio:
   - **BusinessName**: Nombre de tu negocio
   - **Contact.Phone**: Teléfono de contacto
   - **Contact.Email**: Correo electrónico
   - **Contact.Address**: Dirección física

## Configuración de Sucursales

Cada sucursal ahora incluye una imagen de referencia que se muestra en el portal cliente.

### Modelo de Sucursal (Branch)

Las sucursales incluyen las siguientes propiedades:
- **Name**: Nombre de la sucursal
- **Address**: Dirección física
- **Phone**: Teléfono de contacto
- **Email**: Correo electrónico
- **ImageUrl**: URL de la imagen de la sucursal (nueva)

### Cómo Agregar Imágenes a las Sucursales

Las imágenes se configuran mediante URLs. Puedes usar:

1. **Servicios de almacenamiento en la nube**:
   - Amazon S3
   - Azure Blob Storage
   - Cloudinary
   - Unsplash (para imágenes de muestra)

2. **Imágenes locales** (en producción se recomienda usar un CDN):
   - Coloca las imágenes en `wwwroot/images/branches/`
   - Usa la ruta: `/images/branches/nombre-imagen.jpg`

**Ejemplo de URLs**:
```
https://images.unsplash.com/photo-1560066984-138dadb4c035?w=800&h=600&fit=crop
/images/branches/sucursal-central.jpg
```

**Tamaño recomendado de imágenes**: 800x600px o relación de aspecto 4:3

## Contenido "Nosotros"

El contenido de la sección "Nosotros" se gestiona mediante un archivo Markdown ubicado en:

```
AppointmentSystem.WebUI/Content/about.md
```

### Cómo Editar el Contenido

1. Abre el archivo `Content/about.md`
2. Edita el contenido usando formato Markdown
3. Los cambios se reflejarán automáticamente en la aplicación

### Sintaxis Markdown Básica

- `# Título Principal`
- `## Subtítulo`
- `**texto en negrita**`
- `- Lista con viñetas`
- `[texto del enlace](url)`

## Estructura del Menú

El menú principal contiene tres secciones:

1. **Nuestros Servicios** (`/Home/Index`) - Selección de servicios y agendamiento
2. **Nosotros** (`/Home/About`) - Información sobre el negocio (cargada desde Markdown)
3. **Contacto** (`/Home/Contact`) - Información de contacto (cargada desde AppConfig)

## Acceso Administrativo

El botón "Acceso Administrativo" en la esquina superior derecha permite el login para:
- Administradores
- Administradores de Sucursal
- Recepcionistas

## Diseño

El sistema utiliza un diseño **minimalista** con:
- Colores limpios y neutros
- Tipografía clara y legible
- Espaciado generoso
- Transiciones suaves
- Sombras sutiles
- Imágenes de alta calidad para las sucursales

Los colores principales del gradiente (#667eea ? #764ba2) se aplican solo en elementos de acento como botones y enlaces.

## Migraciones de Base de Datos

Si realizas cambios en el modelo de datos (como agregar la propiedad `ImageUrl`), necesitarás aplicar la migración:

```bash
dotnet ef database update --project AppointmentSystem.Infrastructure --startup-project AppointmentSystem.WebUI
```

**Nota**: Asegúrate de que la aplicación no esté en ejecución antes de aplicar migraciones.

