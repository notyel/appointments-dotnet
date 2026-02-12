-- Script SQL para agregar la columna ImageUrl a la tabla Branches
-- Ejecutar este script en la base de datos mientras la aplicación está corriendo

-- Agregar columna ImageUrl
ALTER TABLE "Branches" 
ADD COLUMN "ImageUrl" text NOT NULL DEFAULT '';

-- Actualizar datos de ejemplo con imágenes
UPDATE "Branches" 
SET "ImageUrl" = 'https://images.unsplash.com/photo-1560066984-138dadb4c035?w=800&h=600&fit=crop'
WHERE "Name" = 'Sucursal Central';

UPDATE "Branches" 
SET "ImageUrl" = 'https://images.unsplash.com/photo-1519415510236-718bdfcd89c8?w=800&h=600&fit=crop'
WHERE "Name" = 'Sucursal Norte';

-- Verificar que se aplicó correctamente
SELECT "Id", "Name", "ImageUrl" FROM "Branches";
