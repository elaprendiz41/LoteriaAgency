# Evidencia Hito 1 — RFC 7807

## Archivos

| Archivo | Uso |
|---------|-----|
| `LoteriaAgency-RFC7807.postman_collection.json` | Importar en Postman → Send → capturar pantalla |
| `rfc7807-404-not-found.json` | Respuesta real capturada de la API |

## Pasos captura Postman (obligatorio entrega)

1. API corriendo: `dotnet run --project src/Presentation.API --launch-profile http`
2. Postman → Import → seleccionar la collection
3. Ejecutar **404 Not Found**
4. Verificar `Content-Type: application/problem+json` y body con `type`, `title`, `status`, `detail`, `instance`
5. Screenshot → pegar en entrega / `docs/evidence/postman-404.png`

## Respuesta esperada (404)

```json
{
  "type": "https://httpstatuses.com/404",
  "title": "Recurso No Encontrado",
  "status": 404,
  "detail": "El recurso solicitado no existe.",
  "instance": "/api/demo/errors/not-found"
}
```
