# Task Manager API

Проект представляет собой RESTful API для управления задачами, построенный на основе Clean Architecture с использованием ASP.NET Core, Entity Framework Core и PostgreSQL.

## Архитектура

Проект разделен на следующие слои:

### 1. Domain
- **Сущности**: `Task`, `TaskType`
- **Enums**: `TaskStatus`, `TaskPriority`
- **Интерфейсы**: `IRepository<T>`, `IUnitOfWork`

### 2. Application
- **DTOs**: `TaskDto`, `CreateTaskDto`, `UpdateTaskDto`, `TaskTypeDto`
- **Сервисы**: `TaskService`, `TaskTypeService`
- **Валидаторы**: `CreateTaskDtoValidator`, `UpdateTaskDtoValidator`
- **Маппинг**: `MappingProfile` (AutoMapper)

### 3. Infrastructure
- **Data**: `ApplicationDbContext` (Entity Framework Core)
- **Repositories**: `Repository<T>`, `UnitOfWork`
- **Миграции**: Code-First подход

### 4. API
- **Контроллеры**: `TasksController`, `TaskTypesController`
- **Swagger**: Автоматическая документация API
- **Dependency Injection**: Настройка зависимостей в Program.cs

## Технологии

- **ASP.NET Core 9.0** - веб-фреймворк
- **Entity Framework Core 9.0** - ORM
- **PostgreSQL** - база данных
- **AutoMapper 12.0.1** - маппинг объектов
- **FluentValidation 11.10.0** - валидация DTO
- **Swashbuckle.AspNetCore 7.2.0** - документация Swagger
- **Docker** - контейнеризация PostgreSQL

## Требования

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (для запуска PostgreSQL)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) или [VS Code](https://code.visualstudio.com/)

## Запуск проекта

### 1. Клонирование репозитория
```bash
git clone <repository-url>
cd task-manager
```

### 2. Запуск PostgreSQL через Docker
```bash
docker-compose up -d
```

### 3. Настройка базы данных
Миграции применяются автоматически при запуске приложения через `dbContext.Database.Migrate()` в Program.cs.

### 4. Запуск приложения
```bash
dotnet run --project TaskManager.API
```

Приложение будет доступно по адресу: `https://localhost:5001` (или `http://localhost:5000`)

### 5. Документация API
После запуска откройте в браузере:
- Swagger UI: `http://localhost:5000`
- Swagger JSON: `http://localhost:5000/swagger/v1/swagger.json`

## API Endpoints

### Задачи (Tasks)
- `GET /api/tasks` - получить все задачи
- `GET /api/tasks/{id}` - получить задачу по ID
- `GET /api/tasks/type/{taskTypeId}` - получить задачи по типу
- `POST /api/tasks` - создать новую задачу
- `PUT /api/tasks/{id}` - обновить задачу
- `DELETE /api/tasks/{id}` - удалить задачу

### Типы задач (TaskTypes)
- `GET /api/tasktypes` - получить все типы задач
- `GET /api/tasktypes/{id}` - получить тип задачи по ID

## Структура базы данных

### Таблица Tasks
- `Id` (int, PK)
- `Title` (nvarchar(200), required)
- `Description` (nvarchar(2000), optional)
- `Status` (int, required) - enum: ToDo, InProgress, Done
- `Priority` (int, required) - enum: Low, Medium, High, Critical
- `DueDate` (datetime, optional)
- `TaskTypeId` (int, FK)
- `CreatedAt` (datetime, required)
- `UpdatedAt` (datetime, optional)
- `CompletedAt` (datetime, optional)

### Таблица TaskTypes
- `Id` (int, PK)
- `Name` (nvarchar(100), required, unique)
- `Description` (nvarchar(500), optional)
- `ColorCode` (nvarchar(7), optional)
- `CreatedAt` (datetime, required)
- `UpdatedAt` (datetime, optional)

## Seed данные

При создании базы данных автоматически добавляются 4 типа задач:
1. **Работа** (#3B82F6) - Рабочие задачи
2. **Личное** (#10B981) - Личные задачи
3. **Учеба** (#8B5CF6) - Учебные задачи
4. **Здоровье** (#EF4444) - Задачи связанные со здоровьем

## Архитектурные решения

### 1. Clean Architecture
Проект следует принципам Clean Architecture для обеспечения:
- Независимости бизнес-логики от инфраструктуры
- Легкости тестирования
- Гибкости при изменении технологий

### 2. Repository Pattern + Unit of Work
- `Repository<T>` - generic репозиторий для CRUD операций
- `UnitOfWork` - управление транзакциями и доступ к репозиториям

### 3. Dependency Injection
Все зависимости регистрируются в Program.cs через встроенный DI контейнер.

### 4. Валидация
Используется FluentValidation для валидации входных данных на уровне Application.

### 5. Маппинг
AutoMapper используется для преобразования между сущностями и DTO.

### 6. Миграции
Code-First подход с автоматическим применением миграций при запуске.

## Тестирование

Для запуска тестов:
```bash
dotnet test
```

## Дополнительные инструменты

### Docker Compose
Файл `docker-compose.yml` содержит конфигурацию для PostgreSQL:
- Порт: 5432
- База данных: TaskManagerDB
- Пользователь: postgres
- Пароль: postgres

### .gitignore
Файл `.gitignore` исключает бинарные файлы, настройки IDE и временные файлы.

## Разработка

### Добавление новой сущности
1. Создать сущность в Domain.Entities
2. Создать DTO в Application.DTOs
3. Создать валидатор в Application.Validators
4. Добавить маппинг в MappingProfile
5. Создать репозиторий (если нужен)
6. Создать сервис в Application.Services
7. Создать контроллер в API.Controllers
8. Добавить миграцию: `dotnet ef migrations add <name>`

### Обновление базы данных
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

