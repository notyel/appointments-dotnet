-- =============================================================================
-- Script: update_professionals_data.sql
-- Descripción: Actualiza los datos de los colaboradores/profesionales con
--              Specialty (especialidad) y Description (descripción).
--
-- Proyecto: AppointmentSystem - Centro de Estética Belleza Total
-- Migración base: AddDescriptionAndSpecialtyToProfessional
--
-- IMPORTANTE: Los IDs son generados dinámicamente en el seed (Guid.NewGuid()).
--             Este script usa coincidencia por nombre/email para ser idempotente.
-- =============================================================================

-- ─────────────────────────────────────────────────────────────────────────────
-- 1. Ana García  →  Estilista Principal
-- ─────────────────────────────────────────────────────────────────────────────
UPDATE "Professionals"
SET
	"Specialty"    = 'Estilista Principal',
	"Description"  = 'Especialista en corte y colorimetría con más de 8 años de experiencia. Domina técnicas de balayage, mechas y tratamientos capilares de alta gama.',
	"ImageUrl"     = 'https://images.unsplash.com/photo-1594744803329-e58b31de8bf5?w=400&h=500&fit=crop&crop=face'
WHERE "Email" = 'ana@bellezatotal.com';

-- ─────────────────────────────────────────────────────────────────────────────
-- 2. Carlos Pérez  →  Terapeuta de SPA
-- ─────────────────────────────────────────────────────────────────────────────
UPDATE "Professionals"
SET
	"Specialty"    = 'Terapeuta de SPA',
	"Description"  = 'Certificado en masajes terapéuticos y técnicas de relajación profunda. Experto en aromaterapia, piedras calientes y tratamientos corporales holísticos.',
	"ImageUrl"     = 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=500&fit=crop&crop=face'
WHERE "Email" = 'carlos@bellezatotal.com';

-- ─────────────────────────────────────────────────────────────────────────────
-- 3. Colaboradores adicionales de ejemplo (para demostración del diseño)
--    Insertar solo si no existen, usando la sucursal de branch1 (Sucursal Central)
-- ─────────────────────────────────────────────────────────────────────────────

-- Elena Rodríguez  →  Master Therapist (ejemplo del mockup)
INSERT INTO "Professionals" (
	"Id", "FirstName", "LastName", "Email", "Phone",
	"IsActive", "IsDeleted", "CreatedAt",
	"BranchId",
	"Specialty", "Description", "ImageUrl"
)
SELECT
	gen_random_uuid(),
	'Elena', 'Rodríguez',
	'elena@bellezatotal.com',
	'333',
	true, false, NOW(),
	b."Id",
	'Master Therapist',
	'Especializada en técnicas de tejido profundo y recuperación holística. Combina métodos orientales y occidentales para lograr resultados óptimos en cada sesión.',
	NULL  -- Sin imagen; se mostrará fondo gris en la UI
FROM "Branches" b
WHERE b."Name" = 'Sucursal Central'
  AND NOT EXISTS (
	SELECT 1 FROM "Professionals" WHERE "Email" = 'elena@bellezatotal.com'
  );

-- Lucía Martínez  →  Especialista en Manicura & Pedicura
INSERT INTO "Professionals" (
	"Id", "FirstName", "LastName", "Email", "Phone",
	"IsActive", "IsDeleted", "CreatedAt",
	"BranchId",
	"Specialty", "Description", "ImageUrl"
)
SELECT
	gen_random_uuid(),
	'Lucía', 'Martínez',
	'lucia@bellezatotal.com',
	'444',
	true, false, NOW(),
	b."Id",
	'Especialista en Uñas',
	'Más de 5 años perfeccionando el arte de la manicura y pedicura. Domina nail art, acrílicas, gel y técnicas de semipermanente de las mejores marcas del mercado.',
	NULL
FROM "Branches" b
WHERE b."Name" = 'Sucursal Central'
  AND NOT EXISTS (
	SELECT 1 FROM "Professionals" WHERE "Email" = 'lucia@bellezatotal.com'
  );

-- ─────────────────────────────────────────────────────────────────────────────
-- 4. Asociar nuevos colaboradores con los servicios de la Sucursal Central
-- ─────────────────────────────────────────────────────────────────────────────

-- Elena → Masaje Relajante (BranchService de Sucursal Central)
INSERT INTO "ProfessionalServices" ("ProfessionalId", "BranchServiceId")
SELECT p."Id", bs."Id"
FROM "Professionals" p
JOIN "Branches" br ON br."Id" = p."BranchId"
JOIN "BranchServices" bs ON bs."BranchId" = br."Id"
JOIN "Services" s ON s."Id" = bs."ServiceId"
WHERE p."Email" = 'elena@bellezatotal.com'
  AND s."Name" = 'Masaje Relajante'
  AND br."Name" = 'Sucursal Central'
  AND NOT EXISTS (
	SELECT 1 FROM "ProfessionalServices" ps2
	WHERE ps2."ProfessionalId" = p."Id" AND ps2."BranchServiceId" = bs."Id"
  );

-- Lucía → Manicura (BranchService de Sucursal Central)
INSERT INTO "ProfessionalServices" ("ProfessionalId", "BranchServiceId")
SELECT p."Id", bs."Id"
FROM "Professionals" p
JOIN "Branches" br ON br."Id" = p."BranchId"
JOIN "BranchServices" bs ON bs."BranchId" = br."Id"
JOIN "Services" s ON s."Id" = bs."ServiceId"
WHERE p."Email" = 'lucia@bellezatotal.com'
  AND s."Name" = 'Manicura'
  AND br."Name" = 'Sucursal Central'
  AND NOT EXISTS (
	SELECT 1 FROM "ProfessionalServices" ps2
	WHERE ps2."ProfessionalId" = p."Id" AND ps2."BranchServiceId" = bs."Id"
  );

-- ─────────────────────────────────────────────────────────────────────────────
-- 5. Verificación
-- ─────────────────────────────────────────────────────────────────────────────
SELECT
	p."FirstName" || ' ' || p."LastName" AS "Nombre",
	p."Email",
	p."Specialty"    AS "Especialidad",
	LEFT(p."Description", 60) || '...' AS "Descripción (preview)",
	CASE WHEN p."ImageUrl" IS NOT NULL THEN 'Con imagen' ELSE 'Sin imagen (fondo gris)' END AS "Imagen"
FROM "Professionals" p
ORDER BY p."FirstName";
