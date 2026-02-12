-- Script para actualizar las imágenes de las sucursales existentes
-- Ejecutar después de aplicar la migración AddImageUrlToBranch

UPDATE "Branches" 
SET "ImageUrl" = 'https://images.unsplash.com/photo-1560066984-138dadb4c035?w=800&h=600&fit=crop'
WHERE "Name" = 'Sucursal Central';

UPDATE "Branches" 
SET "ImageUrl" = 'https://images.unsplash.com/photo-1519415510236-718bdfcd89c8?w=800&h=600&fit=crop'
WHERE "Name" = 'Sucursal Norte';

-- Verificar
SELECT "Id", "Name", "ImageUrl" FROM "Branches";
