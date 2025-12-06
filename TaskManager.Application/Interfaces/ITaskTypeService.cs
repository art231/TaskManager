using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces;

public interface ITaskTypeService
{
    Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync();
    Task<TaskTypeDto> GetTaskTypeByIdAsync(int id);
}
