# Patrón Observer (C#)

Ejemplo de micro-video: un **Inventario** (sujeto) avisa automáticamente a sus **suscriptores** (observadores) cada vez que cambia el stock de un producto.

## Qué problema resuelve
Cuando un objeto cambia y otros necesitan enterarse, no hace falta que el objeto los conozca ni los llame uno por uno. Los interesados se suscriben y el objeto solo notifica.

## Estructura
- `IObservador`: contrato que implementan los suscriptores.
- `Inventario`: sujeto; guarda la lista de suscriptores y los notifica.
- `PanelDashboard` y `AlertaCorreo`: observadores concretos.

## Cómo ejecutarlo
Requisito: .NET SDK.

```bash
dotnet run
```

## Salida esperada
```
--- Entra mercadería ---
Inventario: Martillo cambió a 20
  [DASHBOARD] Martillo: 20 unidades

--- Se vende casi todo ---
Inventario: Martillo cambió a 4
  [DASHBOARD] Martillo: 4 unidades
  [CORREO] Stock bajo de Martillo, quedan 4

--- El correo se desuscribe ---
Inventario: Martillo cambió a 2
  [DASHBOARD] Martillo: 2 unidades
```

## Cuándo NO usarlo
Si hay un solo interesado fijo, o si el orden de las notificaciones importa mucho, un llamado directo es más simple y claro. Con muchos observadores también puede volverse difícil de depurar.

## Dónde aparece en full stack
Eventos del DOM (`addEventListener`), `Observable` de RxJS en Angular, SignalR/WebSockets para notificaciones en tiempo real.
