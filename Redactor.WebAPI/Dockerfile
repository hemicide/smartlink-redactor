# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

ENV DB_CONNECTION_STRING="Host=192.168.1.14;Port=5432;Database=postgres;Username=postgres;Password=postgres"

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Redactor.WebAPI/Redactor.WebAPI.csproj", "Redactor.WebAPI/"]
COPY ["Redactor.Application/Redactor.Application.csproj", "Redactor.Application/"]
COPY ["Redactor.Domain/Redactor.Domain.csproj", "Redactor.Domain/"]
COPY ["Redactor.Persistence/Redactor.Repository.csproj", "Redactor.Persistence/"]
COPY ["Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Browser/Redactor.Application.Validators.Browser.csproj", "Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Browser/"]
COPY ["Redactor.Extensions/Redactor.Extensions.csproj", "Redactor.Extensions/"]
COPY ["Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.DateRange/Redactor.Application.Validators.DateRange.csproj", "Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.DateRange/"]
COPY ["Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Device/Redactor.Application.Validators.Device.csproj", "Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Device/"]
COPY ["Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Language/Redactor.Application.Validators.Language.csproj", "Redactor.WebAPI.Validators/Redactor.WebAPI.Validators.Language/"]
RUN dotnet restore "./Redactor.WebAPI/Redactor.WebAPI.csproj"
COPY . .
WORKDIR "/src/Redactor.WebAPI"
RUN dotnet build "./Redactor.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Redactor.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redactor.WebAPI.dll"]