using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDto> GetTaskByIdAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdWithIncludeAsync(id, "TaskType");
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        var taskDto = _mapper.Map<TaskDto>(task);
        taskDto.TaskTypeName = task.TaskType?.Name ?? string.Empty;
        return taskDto;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
    {
        var tasks = await _unitOfWork.Tasks.GetAllWithIncludeAsync("TaskType");
        var taskDtos = new List<TaskDto>();

        foreach (var task in tasks)
        {
            var taskDto = _mapper.Map<TaskDto>(task);
            taskDto.TaskTypeName = task.TaskType?.Name ?? string.Empty;
            taskDtos.Add(taskDto);
        }

        return taskDtos;
    }


    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        // Проверяем существование типа задачи
        var taskType = await _unitOfWork.TaskTypes.GetByIdAsync(createTaskDto.TaskTypeId);
        if (taskType == null)
        {
            throw new KeyNotFoundException($"TaskType with ID {createTaskDto.TaskTypeId} not found.");
        }

        var task = _mapper.Map<Domain.Entities.Task>(createTaskDto);
        task.CreatedAt = DateTime.UtcNow;
        
        await _unitOfWork.Tasks.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        var taskDto = _mapper.Map<TaskDto>(task);
        taskDto.TaskTypeName = taskType.Name;
        return taskDto;
    }

    public async Task<TaskDto> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        // Проверяем существование типа задачи
        var taskType = await _unitOfWork.TaskTypes.GetByIdAsync(updateTaskDto.TaskTypeId);
        if (taskType == null)
        {
            throw new KeyNotFoundException($"TaskType with ID {updateTaskDto.TaskTypeId} not found.");
        }

        _mapper.Map(updateTaskDto, task);
        task.UpdatedAt = DateTime.UtcNow;
        
        // Если статус изменился на Done, устанавливаем CompletedAt
        if (updateTaskDto.Status == Domain.Enums.TaskStatus.Done && task.Status != Domain.Enums.TaskStatus.Done)
        {
            task.CompletedAt = DateTime.UtcNow;
        }
        else if (updateTaskDto.Status != Domain.Enums.TaskStatus.Done)
        {
            task.CompletedAt = null;
        }

        await _unitOfWork.Tasks.UpdateAsync(task);
        await _unitOfWork.SaveChangesAsync();

        var taskDto = _mapper.Map<TaskDto>(task);
        taskDto.TaskTypeName = taskType.Name;
        return taskDto;
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        await _unitOfWork.Tasks.DeleteAsync(task);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> TaskExistsAsync(int id)
    {
        return await _unitOfWork.Tasks.ExistsAsync(id);
    }
}
