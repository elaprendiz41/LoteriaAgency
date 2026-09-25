# LoteriaAgency — Roadmap

Fuente de verdad del proyecto. Chat, canvas y planes son derivados de este documento.

## Visión

POS y gestión operativa para agencias de lotería locales: velocidad en mostrador, control de riesgos (topes), caja auditable, operación resiliente en LAN con conectividad inestable.

## Decisiones de marco

| Decisión | Valor |
|----------|-------|
| Solución | `LoteriaAgency` (`LoteriaAgency.slnx`) |
| Stack | .NET 10 / C# 14, Onion Architecture |
| Capas | `Core.Domain` → `Core.Application` → `Infrastructure` → `Presentation.API` |
| Par dominio H1 | `Draw` + `Ticket` (+ `Play`) |
| Persistencia H1 | Stub / in-memory (Scoped). EF Core en H2–H3 |
| Auth | JWT + RBAC a partir de H2 |

## Asunción NFR (no reabrir cada sprint)

Local-first + topes globales multi-cajero implica **servidor LAN en la agencia** como autoridad de topes. Sin WAN el POS sigue; sin nodo local compartido hay oversell o topes por caja.

## Hitos

| Hito | Fecha / trigger | Estado | Objetivo |
|------|-----------------|--------|----------|
| **H1** | **25 sep 2026** | **done** | Esqueleto académico Onion + RFC 7807 + Draw/Ticket |
| **H2** | Post-rúbrica F1 | **next** | Auth RBAC + Audit mínimo |
| **H3** | Tras H2 | pending | Ventas mostrador (carrito, time-fence, anulación) |
| **H4** | Tras H3 | pending | RiskCap + concurrencia segura |
| **H5** | Tras H4 | pending | Caja: apertura, movimientos, cierre ciego, arqueo |
| **H6** | Tras H5 | pending | Reportes + local-first LAN |

**Puerta:** H1 verde. Siguiente trabajo = H2.

## H1 — Checklist (25 sep 2026) — DONE

- [x] Solution 4 proyectos + referencias Onion
- [x] `Core.Domain` puro (sin PackageReference externos)
- [x] `BaseEntity` (Guid + CreatedAt UTC)
- [x] Entidades `Draw` + `Ticket`/`Play` con relación
- [x] DI nativa Transient / Scoped / Singleton en `Infrastructure.DependencyInjection` + `Program.cs`
- [x] `ExceptionMiddleware` RFC 7807 (404 / 400 / 500, sin stack en 500)
- [x] Endpoint demo: `GET /api/demo/errors/{not-found|bad-request|server-error}`
- [x] README clone + `dotnet build`
- [x] `.gitignore` sin bin/obj
- [ ] Captura Postman problem+json (**manual — estudiante**)

### Verificación automática H1

- `dotnet build LoteriaAgency.slnx` — 0 errores
- Smoke HTTP: 404/400/500 → `application/problem+json` con `type`, `title`, `status`, `detail`, `instance`; 500 sin stack

## H2 — Siguiente (pendiente)

- `User` + roles Cajero / Admin (/ Auditor read-only)
- JWT + policies en API
- `AuditEntry` en writes sensibles y login fallido
- (Opcional) introducir EF Core + PostgreSQL/SQLite

## Fuera de H1 (histórico)

Auth JWT, EF Core, caja ciega, RiskCap concurrente, UI React keyboard-first, sync nube, sello criptográfico de ticket, dashboards en vivo → H2+.

## Criterio de éxito H1

- [x] `dotnet build` limpio
- [x] Domain sin dependencias externas
- [x] API: 404/400/500 → `application/problem+json`
- [x] README usable por un tercero
- [x] ROADMAP marca H1 done / H2 next
- [ ] Evidencia Postman (manual)
