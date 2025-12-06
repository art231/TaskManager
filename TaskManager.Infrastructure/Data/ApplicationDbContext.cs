using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=TaskManagerDB;Username=postgres;Password=postgres");
        }
        // Подавление предупреждения о pending changes
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }

    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация сущности Task
        modelBuilder.Entity<Domain.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Priority).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.CompletedAt);
            
            // Связь с TaskType
            entity.HasOne(e => e.TaskType)
                .WithMany(t => t.Tasks)
                .HasForeignKey(e => e.TaskTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфигурация сущности TaskType
        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ColorCode).HasMaxLength(7);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            
            // Индекс для быстрого поиска по имени
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Seed данные для TaskType
        modelBuilder.Entity<TaskType>().HasData(
            new TaskType
            {
                Id = 1,
                Name = "Работа",
                Description = "Рабочие задачи",
                ColorCode = "#3B82F6",
                CreatedAt = DateTime.UtcNow
            },
            new TaskType
            {
                Id = 2,
                Name = "Личное",
                Description = "Личные задачи",
                ColorCode = "#10B981",
                CreatedAt = DateTime.UtcNow
            },
            new TaskType
            {
                Id = 3,
                Name = "Учеба",
                Description = "Учебные задачи",
                ColorCode = "#8B5CF6",
                CreatedAt = DateTime.UtcNow
            },
            new TaskType
            {
                Id = 4,
                Name = "Здоровье",
                Description = "Задачи связанные со здоровьем",
                ColorCode = "#EF4444",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
