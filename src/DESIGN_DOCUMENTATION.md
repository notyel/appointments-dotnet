# Documentación de Estructura CSS

## Iconografía

El sistema utiliza **Material Symbols Outlined de Google** con carga optimizada para mantener una apariencia profesional y un rendimiento óptimo.

### ? Implementación Optimizada

**Problema evitado:** Cargar toda la librería de Material Icons pesa 7.9 MB y contiene más de 3,800 iconos innecesarios.

**Solución:** Estructura en dos niveles:
1. **Layouts** cargan iconos comunes (logo, navegación)
2. **Vistas** cargan solo iconos adicionales específicos

### ?? Estructura de Carga

**Layout Cliente (`_Layout.cshtml`):**
```html
<!-- Iconos comunes del portal cliente -->
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght@400&icon_names=spa,home,event,account_circle" rel="stylesheet" />
```

**Layout Admin (`_LayoutAdmin.cshtml`):**
```html
<!-- Iconos comunes del portal admin -->
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght@400&icon_names=dashboard,account_circle,business,list_alt,people,event_note,assessment,settings,home" rel="stylesheet" />
```

**Vistas (solo iconos adicionales):**
```razor
@section Styles {
    <!-- Iconos adicionales para esta vista -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght@400&icon_names=arrow_back,check_circle" rel="stylesheet" />
}
```

**Clase de iconos:**
```html
<span class="material-symbols-outlined">home</span>
```

**Sintaxis correcta del parámetro `icon_names`:**
- Valores separados por comas
- Sin espacios: `icon_names=home,search,settings`
- Parámetro `wght@400` para peso normal (o `wght@100` para más ligero)

### ?? Iconos por Componente

**Layout Cliente (iconos base):**
- `spa` - Logo del portal
- `home` - Navegación inicio
- `event` - Navegación citas
- `account_circle` - Usuario

**Vistas del Portal Cliente (adicionales):**
- Home/Index: `waving_hand`, `phone`
- Services: `schedule`, `local_offer`, `arrow_back`
- Professionals: `arrow_back`
- Slots: `warning`, `arrow_back`
- Create: `assignment`, `arrow_back`, `check_circle`
- Confirmation: `check_circle`, `assignment`, `email`, `arrow_forward`

**Layout Admin (iconos base):**
- `dashboard` - Logo
- `account_circle` - Usuario
- `business` - Sucursales (nav)
- `list_alt` - Servicios (nav)
- `people` - Personal (nav)
- `event_note` - Citas (nav)
- `assessment` - Reportes (nav)
- `settings` - Configuración (nav)
- `home` - Sitio público (nav)

**Vistas del Portal Admin (adicionales):**
- Index: `location_on`, `phone`
- Dashboard: `arrow_back`, `check_circle`, `schedule`

### ?? Ventajas de esta Implementación

1. **Performance:** ~5-15 KB por vista (vs 7.9 MB completo)
2. **Mantenibilidad:** Iconos comunes en layout, específicos en vistas
3. **DRY (Don't Repeat Yourself):** Sin duplicación de iconos
4. **Carga Progresiva:** Layout carga primero, luego iconos específicos
5. **Escalabilidad:** Fácil agregar nuevos iconos sin duplicar comunes

### ?? Iconos Principales Utilizados

- `spa` - Logo estética/belleza
- `home` - Inicio
- `event` - Citas/eventos
- `account_circle` - Usuario
- `business` - Sucursales
- `people` - Personal
- `list_alt` - Servicios
- `schedule` - Horarios
- `phone` - Teléfono
- `location_on` - Ubicación
- `check_circle` - Confirmación
- `arrow_back` - Volver
- `arrow_forward` - Siguiente
- `settings` - Configuración
- `assessment` - Reportes
- `dashboard` - Panel
- `email` - Correo
- `warning` - Advertencias
- `local_offer` - Categorías
- `assignment` - Asignaciones
- `waving_hand` - Saludo
- `event_note` - Notas de citas

---

## Organización del Diseño

El diseño de la aplicación ha sido completamente refactorizado y organizado por dominio funcional. Los estilos CSS están divididos en tres archivos principales:

### 1. **site.css** - CSS Base Global
Ubicación: `wwwroot/css/site.css`

Contiene estilos reutilizables comunes para ambos portales:
- ? Variables CSS globales (colores, espaciado, tipografía)
- ? Formularios (labels, inputs, textareas, selects)
- ? Botones (primary, secondary, success, danger, link)
- ? Validaciones y mensajes de error
- ? Alertas (success, error, warning, info)
- ? Utilidades comunes (contenedores, márgenes)

**Características principales:**
- Reset CSS básico
- Sistema de diseño basado en variables CSS
- Estados de formularios (focus, disabled, error, success)
- Botones con estados hover y disabled
- Mensajes de validación estilizados

---

### 2. **client-portal.css** - Portal Cliente
Ubicación: `wwwroot/css/client-portal.css`

Estilos específicos para el flujo de reservas del cliente.

**Componentes principales:**

#### ?? Flujo Step-by-Step (Stepper)
- Indicador visual de progreso con 5 pasos
- Estados: completado, activo, pendiente
- Barra de progreso animada
- Responsive para mobile

#### ?? Tarjetas de Contenido
- `.step-content-card` - Contenedor principal de cada paso
- `.step-title` - Título del paso actual
- `.step-description` - Descripción del paso

#### ?? Grid de Opciones
- `.options-grid` - Grid responsive para sucursales/servicios
- `.option-card` - Tarjeta individual con hover effects
- `.option-card-badge` - Etiqueta para precios
- `.option-card-meta` - Metadata (duración, categoría, etc.)

#### ?? Profesionales
- `.professionals-grid` - Grid específico para profesionales
- `.professional-card` - Tarjeta de profesional
- `.professional-avatar` - Avatar circular con iniciales

#### ? Selector de Horarios
- `.date-selector` - Selector de fecha
- `.time-slots-grid` - Grid de horarios disponibles
- `.time-slot` - Botón individual de horario

#### ?? Navegación entre Pasos
- `.step-navigation` - Barra de navegación inferior
- Botones "Volver" y "Continuar"

#### ?? Resumen de Selección
- `.selection-summary` - Resumen de la cita
- `.selection-summary-item` - Item individual del resumen

**Pasos del flujo:**
1. Selección de Sucursal
2. Selección de Servicio
3. Selección de Profesional
4. Selección de Fecha y Hora
5. Confirmación de Datos

---

### 3. **admin-portal.css** - Portal Administrativo
Ubicación: `wwwroot/css/admin-portal.css`

Estilos específicos para el panel de administración.

**Componentes principales:**

#### ?? Layout Administrativo
- `.admin-layout` - Contenedor principal
- `.admin-header` - Header con gradiente
- `.admin-sidebar` - Sidebar de navegación sticky
- `.admin-main` - Área de contenido principal

#### ?? Navegación
- `.admin-nav` - Contenedor de navegación
- `.admin-nav-item` - Item de navegación
- `.admin-nav-item.active` - Item activo
- Estados hover y active

#### ?? Header de Página
- `.page-header` - Encabezado de página
- `.page-title` - Título principal
- `.page-subtitle` - Subtítulo
- `.page-actions` - Acciones de página

#### ?? Estadísticas
- `.stats-grid` - Grid de estadísticas
- `.stat-card` - Tarjeta de estadística
- `.stat-card-icon` - Icono con variantes (primary, success, warning, info)
- `.stat-card-value` - Valor numérico destacado

#### ?? Paneles
- `.panel-card` - Tarjeta de contenido
- `.panel-card-header` - Header del panel
- `.panel-card-body` - Cuerpo del panel
- `.panel-card-footer` - Footer del panel

#### ?? Tablas de Datos
- `.data-table` - Tabla de datos estilizada
- Estados hover en filas
- Headers con estilo diferenciado

#### ??? Badges
- `.badge` - Badge base
- Variantes: `badge-primary`, `badge-success`, `badge-warning`, `badge-error`, `badge-info`

#### ?? Grid de Items Admin
- `.admin-items-grid` - Grid responsive para items administrativos
- `.admin-item-card` - Tarjeta de item con hover effects

#### ?? Modales
- `.modal-overlay` - Overlay con blur
- `.modal` - Contenedor del modal
- `.modal-header`, `.modal-body`, `.modal-footer`

---

## Layouts

### Layout Cliente (`_Layout.cshtml`)
- Header con logo y navegación
- Información de usuario autenticado
- Contenedor principal con gradiente de fondo
- Footer simple

### Layout Admin (`_LayoutAdmin.cshtml`)
- Header con gradiente
- Sidebar de navegación lateral sticky
- Contenido principal flexible
- Navegación con iconos

---

## Componente Reutilizable

### `_BookingStepper.cshtml`
Componente parcial que muestra el indicador de progreso del flujo de reservas.

**Uso:**
```razor
@{
    ViewBag.CurrentStep = 2; // Paso actual (1-5)
}

<partial name="_BookingStepper" />
```

**Pasos:**
1. Sucursal
2. Servicio
3. Profesional
4. Fecha y Hora
5. Confirmación

---

## Variables CSS Principales

```css
/* Colores */
--color-primary: #4f46e5;
--color-success: #10b981;
--color-error: #ef4444;
--color-warning: #f59e0b;
--color-info: #3b82f6;

/* Espaciado */
--spacing-sm: 0.5rem;
--spacing-md: 1rem;
--spacing-lg: 1.5rem;
--spacing-xl: 2rem;

/* Tipografía */
--font-size-sm: 0.875rem;
--font-size-base: 1rem;
--font-size-lg: 1.125rem;
--font-size-xl: 1.25rem;

/* Sombras */
--shadow-sm: 0 1px 2px 0 rgba(0, 0, 0, 0.05);
--shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
--shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.1);

/* Border Radius */
--radius-sm: 0.25rem;
--radius-md: 0.5rem;
--radius-lg: 0.75rem;
--radius-xl: 1rem;
```

---

## Responsive Design

Todos los componentes son responsive y se adaptan a diferentes tamaños de pantalla:

- **Desktop**: Vista completa con todos los elementos
- **Tablet** (< 1024px): Sidebar colapsable en admin
- **Mobile** (< 768px): 
  - Grids a una columna
  - Navegación apilada
  - Botones de ancho completo
  - Stepper con labels reducidos

---

## Mejores Prácticas

### 1. Uso de Variables CSS
Siempre usar variables CSS para colores, espaciado y tipografía:
```css
/* ? Correcto */
padding: var(--spacing-md);
color: var(--color-primary);

/* ? Incorrecto */
padding: 1rem;
color: #4f46e5;
```

### 2. Nomenclatura de Clases
- Usar BEM-like para componentes complejos
- Prefijos descriptivos: `admin-`, `client-`, `step-`
- Evitar nombres genéricos como `.card`, usar `.option-card`, `.stat-card`, etc.

### 3. Estados
- Usar pseudo-clases para estados hover, focus, disabled
- Añadir transiciones suaves con `var(--transition-base)`
- Proporcionar feedback visual en todas las interacciones

### 4. Accesibilidad
- Contraste de colores adecuado
- Focus visible en elementos interactivos
- Tamaños de toque adecuados (mínimo 44x44px)
- Labels descriptivos en formularios

---

## Estructura de Archivos

```
AppointmentSystem.WebUI/
??? wwwroot/
?   ??? css/
?       ??? site.css              # CSS Base Global
?       ??? client-portal.css     # Portal Cliente
?       ??? admin-portal.css      # Portal Administrativo
??? Views/
?   ??? Shared/
?   ?   ??? _Layout.cshtml        # Layout Cliente
?   ?   ??? _LayoutAdmin.cshtml   # Layout Admin
?   ?   ??? _BookingStepper.cshtml # Componente Stepper
?   ??? Home/
?   ?   ??? Index.cshtml          # Paso 1: Sucursales
?   ??? Booking/
?   ?   ??? Services.cshtml       # Paso 2: Servicios
?   ?   ??? Professionals.cshtml  # Paso 3: Profesionales
?   ?   ??? Slots.cshtml          # Paso 4: Horarios
?   ?   ??? Create.cshtml         # Paso 5: Confirmación
?   ?   ??? Confirmation.cshtml   # Éxito
?   ??? Admin/
?       ??? Index.cshtml          # Lista de Sucursales
?       ??? Dashboard.cshtml      # Dashboard por Sucursal
```

---

## Próximas Mejoras Sugeridas

1. **Animaciones**: Añadir transiciones entre pasos
2. **Dark Mode**: Implementar tema oscuro usando variables CSS
3. **Iconos**: Reemplazar emojis con librería de iconos (Font Awesome, etc.)
4. **Feedback Visual**: Añadir spinners y estados de carga
5. **Toast Notifications**: Sistema de notificaciones temporales
6. **Validación en Tiempo Real**: Mejorar feedback de formularios

---

## Notas de Migración

### Cambios Principales
- ? Eliminado: `site.css` antiguo con estilos mezclados
- ? Creado: Estructura modular por dominio
- ? Creado: Componente stepper reutilizable
- ? Actualizado: Todas las vistas del flujo cliente
- ? Actualizado: Vistas administrativas

### Compatibilidad
- ? Compatible con .NET 8
- ? Compatible con navegadores modernos
- ? Mobile-first responsive design
- ? Material Symbols Outlined (optimizado, carga por vista)
- ? Sin dependencias externas de CSS adicionales
- ? Performance optimizado (~10-20 KB de iconos por vista)
