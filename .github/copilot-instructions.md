\# Commit Message Convention



When generating commit messages, you MUST strictly follow this format:



type(scope): descripción



\## 1. General Rules



\- The commit `type` MUST be in English.

\- The `description` MUST be in Spanish.

\- The description MUST be written in imperative mood.

\- The subject line MUST NOT exceed 72 characters.

\- Do NOT add emojis.

\- Do NOT add unnecessary explanations.

\- Keep the first line concise and meaningful.



---



\## 2. Allowed Types (in English)



\- feat → Nueva funcionalidad

\- fix → Corrección de errores

\- docs → Cambios en documentación

\- style → Cambios de formato sin afectar lógica

\- refactor → Refactorización sin agregar features ni corregir bugs

\- perf → Mejoras de rendimiento

\- test → Agregar o actualizar pruebas

\- chore → Cambios de mantenimiento

\- ci → Cambios en pipelines o integración continua



---



\## 3. Scope Rules



\- The `scope` is optional but recommended.

\- Must be lowercase.

\- Should represent the affected area (examples: api, ui, auth, branch, appointments, infrastructure).



Example:



feat(api): agregar validación de disponibilidad por sucursal



---



\## 4. Multi-line Commits



If more detail is required:



\- Add a blank line after the first line.

\- Use bullet points.

\- Write bullet points in Spanish.

\- Keep them concise.



Example:



feat(appointments): agregar validación de solapamiento de horarios (#45)



\- Implementar validación StartA < EndB \&\& EndA > StartB

\- Bloquear reservas que excedan capacidad simultánea

\- Agregar mensaje de error descriptivo



---



\## 5. Issue References



When applicable, reference issues using:



(#issueNumber)



Example:



fix(ui): corregir validación del formulario de reservas (#102)



---



\## 6. Strict Enforcement



Do NOT generate commit messages that:

\- Are written entirely in English.

\- Exceed 72 characters in the subject line.

\- Use past tense.

\- Omit the type.

\- Use invalid types.



