using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services;

public class TaskTypeService : ITaskTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
    {
        var taskTypes = await _unitOfWork.TaskTypes.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskTypeDto>>(taskTypes);
    }

    public async Task<TaskTypeDto> GetTaskTypeByIdAsync(int id)
    {
        var taskType = await _unitOfWork.TaskTypes.GetByIdAsync(id);
        if (taskType == null)
        {
            throw new KeyNotFoundException($"TaskType with ID {id} not found.");
        }

        return _mapper.Map<TaskTypeDto>(taskType);
    }
}
