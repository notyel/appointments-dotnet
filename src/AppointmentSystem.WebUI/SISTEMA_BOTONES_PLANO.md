# Sistema de Botones Unificado - Diseño Plano

## ?? Cambios Realizados

Se ha unificado el sistema de botones para lograr **diseño plano consistente** en todos los steps y vistas de la aplicación.

---

## ? Antes (Diseño No Plano)

### Problemas:
- ? Efectos `transform: translateY(-1px)` en hover
- ? `box-shadow` en hover (elevación)
- ? Bordes visibles en algunos botones
- ? Inconsistencia entre steps
- ? Padding pequeño e inconsistente

### Ejemplo Anterior:
```css
.btn-primary:hover {
    transform: translateY(-1px);  /* ? Elevación */
    box-shadow: var(--shadow-md); /* ? Sombra */
}
```

---

## ? Ahora (Diseño Plano Unificado)

### Características:
- ? **Sin elevación** (no transform)
- ? **Sin sombras** (diseño plano)
- ? **Sin bordes** (border: none)
- ? **Solo cambio de color** en hover/active
- ? **Padding consistente** en todos los steps
- ? **Transiciones suaves** solo en opacidad y color

### Botón Base:
```css
.btn {
    padding: var(--spacing-md) var(--spacing-xl); /* 16px 32px */
    font-weight: var(--font-weight-semibold);     /* 600 */
    border: none;                                  /* Sin borde */
    border-radius: var(--radius-lg);              /* 8px */
    transition: background-color, opacity;         /* Solo color */
}
```

---

## ?? Variantes de Botones

### 1. Botón Primario (`.btn-primary`)

**Uso:** Acciones principales, siguiente paso, confirmar

```html
<button class="btn btn-primary">Seleccionar</button>
```

**Colores:**
- Normal: `#667eea` (morado claro)
- Hover: `#5568d3` (morado más oscuro)
- Active: `#5568d3` con opacidad 0.9

**Estados:**
```css
/* Normal */
background-color: #667eea;
color: white;

/* Hover */
background-color: #5568d3; /* Solo cambia color */
color: white;

/* Active (click) */
background-color: #5568d3;
opacity: 0.9;
```

---

### 2. Botón Secundario (`.btn-secondary`)

**Uso:** Volver, cancelar, acciones secundarias

```html
<button class="btn btn-secondary">Volver</button>
```

**Colores:**
- Normal: `#f8f9fa` (gris muy claro)
- Hover: `#e0e0e0` (gris borde)
- Borde: `1px solid #e0e0e0`

---

### 3. Botón Éxito (`.btn-success`)

**Uso:** Confirmar, guardar, completar

```html
<button class="btn btn-success">Confirmar</button>
```

**Colores:**
- Normal: `#10b981` (verde)
- Hover: `#059669` (verde oscuro)

---

### 4. Botón Peligro (`.btn-danger`)

**Uso:** Eliminar, cancelar cita

```html
<button class="btn btn-danger">Eliminar</button>
```

**Colores:**
- Normal: `#ef4444` (rojo)
- Hover: `#dc2626` (rojo oscuro)

---

### 5. Botón Link (`.btn-link`)

**Uso:** Enlaces que parecen botones

```html
<button class="btn btn-link">Más información</button>
```

**Características:**
- Sin fondo
- Color morado
- Subrayado en hover

---

## ?? Tamaños de Botones

### Pequeño (`.btn-sm`)

```html
<button class="btn btn-primary btn-sm">Pequeño</button>
```

- Padding: `8px 24px`
- Font-size: `14px`
- Font-weight: `500` (medium)

---

### Normal (sin modificador)

```html
<button class="btn btn-primary">Normal</button>
```

- Padding: `16px 32px`
- Font-size: `16px`
- Font-weight: `600` (semibold)

---

### Grande (`.btn-lg`)

```html
<button class="btn btn-primary btn-lg">Grande</button>
```

- Padding: `24px 48px`
- Font-size: `18px`
- Font-weight: `600` (semibold)

---

### Ancho Completo (`.btn-block`)

```html
<button class="btn btn-primary btn-block">Ancho completo</button>
```

- Display: `flex`
- Width: `100%`

---

## ?? Consistencia en Steps

### Step 1: Selección de Sucursal
```html
<a class="btn btn-primary btn-block">Seleccionar Sucursal</a>
```

### Step 2: Selección de Servicio
```html
<a class="btn btn-primary btn-block">Seleccionar Servicio</a>
```

### Step 3: Selección de Profesional
```html
<a class="btn btn-primary btn-block">Seleccionar Profesional</a>
```

### Step 4: Selección de Horario
```html
<a class="btn btn-primary btn-block">Confirmar Horario</a>
```

### Navegación (Volver)
```html
<a class="btn btn-secondary">
    <span class="material-symbols-outlined">arrow_back</span>
    <span>Volver</span>
</a>
```

---

## ?? Principios del Diseño Plano

### ? Lo que HACE el diseño plano:
1. **Colores sólidos** sin gradientes (excepto el gradiente principal de marca)
2. **Sin sombras** en estado normal
3. **Sin efectos 3D** (elevación, profundidad)
4. **Cambios sutiles** solo en hover (color más oscuro)
5. **Transiciones rápidas** (200ms)
6. **Bordes limpios** con border-radius consistente

### ? Lo que NO hace el diseño plano:
1. ~~Transform (translateY)~~
2. ~~Box-shadow~~
3. ~~Gradientes en botones~~
4. ~~Bordes gruesos~~
5. ~~Efectos de relieve~~

---

## ?? Comparativa Visual

### Antes
```
???????????????????
?  Seleccionar    ? ? Hover: se eleva y tiene sombra
???????????????????
     ? 2px elevación
  ?? box-shadow
```

### Ahora (Plano)
```
???????????????????
?  Seleccionar    ? ? Hover: solo cambia de color
???????????????????
    Sin elevación
    Sin sombra
    Solo cambio de color: #667eea ? #5568d3
```

---

## ?? Cómo Usar en Vistas

### Ejemplo Completo en Step

```html
<div class="content">
    <div class="step-header">
        <h1 class="step-title">Seleccione un Servicio</h1>
        <p class="step-description">Elija el servicio que desea</p>
    </div>

    <div class="options-grid">
        <!-- Opción 1 -->
        <div class="option-card">
            <h3>Corte de Cabello</h3>
            <p>30 minutos - $25</p>
            <a href="#" class="btn btn-primary btn-block">
                Seleccionar
            </a>
        </div>

        <!-- Opción 2 -->
        <div class="option-card">
            <h3>Manicura</h3>
            <p>45 minutos - $35</p>
            <a href="#" class="btn btn-primary btn-block">
                Seleccionar
            </a>
        </div>
    </div>

    <!-- Navegación -->
    <div class="step-navigation">
        <a href="#" class="btn btn-secondary">
            <span class="material-symbols-outlined">arrow_back</span>
            <span>Volver</span>
        </a>
        <div></div> <!-- Espaciador -->
    </div>
</div>
```

---

## ?? Errores Comunes a Evitar

### ? NO hacer:

```html
<!-- NO: Estilos inline -->
<button style="background: #667eea; padding: 10px;">Click</button>

<!-- NO: Clases múltiples incorrectas -->
<button class="btn btn-primary btn-secondary">Click</button>

<!-- NO: Agregar sombras manualmente -->
<button class="btn btn-primary" style="box-shadow: 0 4px 8px;">Click</button>
```

### ? SÍ hacer:

```html
<!-- SÍ: Usar clases del sistema -->
<button class="btn btn-primary">Click</button>

<!-- SÍ: Combinar con tamaños -->
<button class="btn btn-primary btn-lg">Click</button>

<!-- SÍ: Botón con ícono -->
<button class="btn btn-primary">
    <span class="material-symbols-outlined">check</span>
    <span>Confirmar</span>
</button>
```

---

## ?? Checklist para Desarrolladores

Al crear un nuevo step o formulario:

```
? Usar .btn .btn-primary para acción principal
? Usar .btn .btn-secondary para volver/cancelar
? Usar .btn-block para botones de ancho completo
? NO agregar estilos inline de color o sombra
? NO usar transform o box-shadow custom
? Verificar que el botón sea consistente con otros steps
? Probar estados: normal, hover, active, disabled
```

---

## ?? Paleta de Botones

| Clase | Color Normal | Color Hover | Uso |
|-------|-------------|-------------|-----|
| `.btn-primary` | #667eea | #5568d3 | Acción principal |
| `.btn-secondary` | #f8f9fa | #e0e0e0 | Cancelar, volver |
| `.btn-success` | #10b981 | #059669 | Confirmar, guardar |
| `.btn-danger` | #ef4444 | #dc2626 | Eliminar, borrar |
| `.btn-link` | Transparent | Underline | Enlaces |

---

## ?? Ventajas del Diseño Plano

### Rendimiento
- ? Menos CSS (sin box-shadow complejas)
- ? Transiciones más rápidas (solo color)
- ? Menor repaint en hover

### UX/UI
- ? Más moderno y minimalista
- ? Consistencia visual total
- ? Fácil de escanear visualmente
- ? Funciona mejor en pantallas pequeñas

### Desarrollo
- ? Fácil de mantener
- ? Menos código CSS
- ? Más predecible
- ? Más fácil de debuggear

---

**Fecha de Implementación:** 2026-02-11  
**Versión:** 2.0 - Diseño Plano Unificado  
**Archivos Afectados:** `forms.css`  
**Estado:** ? Listo para producción
