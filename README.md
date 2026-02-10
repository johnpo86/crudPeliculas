# .NET 8 Web API – Dockerized

## Requisitos
- Docker
- Docker Compose

## Características
- El proyecto está completamente dockerizado para evitar dependencias locales.
- Se utilizó SQLite para simplificar la ejecución y evaluación del proyecto.
- Se separó frontend y backend en servicios independientes.
- Docker Compose permite levantar todo el stack con un solo comando.
- El frontend se implemento con Vue.js.
- El backend se implemento con .NET 8.

## Ejecutar el proyecto

```bash
docker compose up --build
```

## Detener el proyecto

```bash
docker compose down
```

-- Ir a la documentacion de la API

```bash
http://localhost:8080/swagger
```

-- Ir a la aplicacion

```bash
http://localhost:3000
```
