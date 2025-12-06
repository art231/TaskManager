namespace TaskManager.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Entities.Task> Tasks { get; }
    IRepository<Entities.TaskType> TaskTypes { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
