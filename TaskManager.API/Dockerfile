# Используем официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем файлы решения и проектов
COPY TaskManager.sln ./
COPY TaskManager.Domain/TaskManager.Domain.csproj ./TaskManager.Domain/
COPY TaskManager.Application/TaskManager.Application.csproj ./TaskManager.Application/
COPY TaskManager.Infrastructure/TaskManager.Infrastructure.csproj ./TaskManager.Infrastructure/
COPY TaskManager.API/TaskManager.API.csproj ./TaskManager.API/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем весь исходный код
COPY . .

# Собираем приложение
WORKDIR /src/TaskManager.API
RUN dotnet publish -c Release -o /app/publish

# Используем образ runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Копируем собранное приложение
COPY --from=build /app/publish .

# Устанавливаем переменную среды для ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
