# Redactor

Сервис редактора.

## Подготовка

Создание Volume:

```sh
docker volume create postgres_volume
```

Загрузка и запуск Postgres:

```sh
docker run -d --name postgres_smartlinks -p 5432:5432 -v postgres_volume:/var/lib/postgresql/data -e POSTGRES_PASSWORD=postgres -t postgres
```

Генерация сущностей. Реверс-инженеринг

```ps
Scaffold-DbContext "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL  -schema Public -table smartlinks -project ani_model -OutputDir "Entities" -ContextDir "Infrastructure\EntityFramework\Contexts" -NoPluralize -NoOnConfiguring -Context ModelContext
```

## Сборка

Создание Docker-образа [Dockerfile](/Redactor.WebAPI/Dockerfile)

Environment variables:

```sh
"ASPNETCORE_HTTPS_PORTS": "8081",
"ASPNETCORE_HTTP_PORTS": "8080",
"DB_CONNECTION_STRING":"Host=<address>>;Port=5432;Database=postgres;Username=postgres;Password=postgres"
```
