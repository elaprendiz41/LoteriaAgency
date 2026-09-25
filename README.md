# LoteriaAgency

Sistema POS / gestión operativa para agencias de lotería locales.

Arquitectura: **Onion** sobre **.NET 10** (C#). Capas:

| Proyecto | Rol |
|----------|-----|
| `Core.Domain` | Entidades puras (`BaseEntity`, `Draw`, `Ticket`, `Play`) — sin NuGet externos |
| `Core.Application` | Abstracciones (`IClock`, `IDrawRepository`, `IDemoErrorService`) |
| `Infrastructure` | Stubs in-memory + registro DI |
| `Presentation.API` | Controllers, `ExceptionMiddleware` (RFC 7807) |

Fuente de verdad del roadmap: [`docs/ROADMAP.md`](docs/ROADMAP.md).

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Clonar y compilar

```bash
git clone <url-del-repositorio>
cd osman   # o el nombre de la carpeta del clone
dotnet restore LoteriaAgency.slnx
dotnet build LoteriaAgency.slnx
```

## Ejecutar la API

```bash
dotnet run --project src/Presentation.API
```

Por defecto escucha en los puertos de `src/Presentation.API/Properties/launchSettings.json` (HTTP/HTTPS).

OpenAPI (Development): `/openapi/v1.json`

## Demostración RFC 7807 (Postman / curl)

El middleware captura excepciones y responde `application/problem+json`:

| Endpoint | Excepción | HTTP |
|----------|-----------|------|
| `GET /api/demo/errors/not-found` | `KeyNotFoundException` | 404 |
| `GET /api/demo/errors/bad-request` | `InvalidOperationException` | 400 |
| `GET /api/demo/errors/server-error` | `Exception` | 500 (sin stack trace) |

Ejemplo:

```bash
curl -i http://localhost:5XXX/api/demo/errors/not-found
```

Respuesta esperada (campos RFC 7807): `type`, `title`, `status`, `detail`, `instance`.

## Inyección de dependencias (Hito 1)

Registradas en `Infrastructure.DependencyInjection`:

- **Singleton** — `IClock` → `SystemClock`
- **Transient** — `IDemoErrorService` → `DemoErrorService`
- **Scoped** — `IDrawRepository` → `InMemoryDrawRepository`

## Hito actual

**H1 (25 sep 2026):** esqueleto Onion + dominio Lotería + ExceptionMiddleware. Ver roadmap para H2+.
