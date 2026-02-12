# Componentes CSS

Esta carpeta contiene los estilos CSS organizados por componente. Cada componente reutilizable debe tener su propio archivo CSS independiente.

## Estructura

```
/css/components/
??? steps.css          # Componente de indicador de progreso (stepper)
??? README.md          # Este archivo
```

## Convenciones

1. **Un archivo por componente**: Cada componente debe tener su propio archivo CSS.
2. **Nombres descriptivos**: Los nombres de archivo deben reflejar claramente el componente que representan.
3. **Sin duplicidad**: No debe existir código CSS duplicado entre archivos.
4. **Variables CSS**: Usar siempre las variables CSS definidas en `site.css` para mantener consistencia.
5. **Comentarios claros**: Cada archivo debe tener un encabezado que describa el componente.

## Componentes Actuales

### `steps.css`
Estilos completos para el sistema de pasos del flujo de reserva de citas.

**Clases incluidas:**
- `.booking-stepper` - Contenedor principal del indicador de progreso
- `.stepper-container` - Contenedor de los pasos individuales
- `.stepper-progress` - Barra de progreso animada
- `.stepper-step` - Paso individual
- `.stepper-step-number` - Círculo con número o check
- `.stepper-step-label` - Etiqueta del paso
- `.step-header` - Encabezado de cada página de paso
- `.step-title` - Título principal de cada paso
- `.step-description` - Descripción del paso
- `.step-navigation` - Contenedor de navegación (botones Volver/Continuar)

**Uso:** 
```razor
<partial name="_BookingStepper" />
<div class="step-header">
    <h1 class="step-title">Título</h1>
    <p class="step-description">Descripción</p>
</div>
```

## Agregar un Nuevo Componente

1. Crear un nuevo archivo `.css` en esta carpeta
2. Agregar la referencia en `_Layout.cshtml` o `_LayoutAdmin.cshtml` según corresponda
3. Documentar el componente en este README

## Orden de Carga Recomendado

Los CSS deben cargarse en el siguiente orden en los layouts:

1. `site.css` - Variables y estilos base
2. `forms.css` - Sistema de formularios
3. `components/*.css` - Componentes reutilizables
4. Portal específicos (`client-portal.css`, `admin-portal.css`)
