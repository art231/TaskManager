using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTypesController : ControllerBase
{
    private readonly ITaskTypeService _taskTypeService;

    public TaskTypesController(ITaskTypeService taskTypeService)
    {
        _taskTypeService = taskTypeService;
    }

    /// <summary>
    /// Получить все типы задач
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTaskTypes()
    {
        var taskTypes = await _taskTypeService.GetAllTaskTypesAsync();
        return Ok(taskTypes);
    }

    /// <summary>
    /// Получить тип задачи по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskTypeById(int id)
    {
        try
        {
            var taskType = await _taskTypeService.GetTaskTypeByIdAsync(id);
            return Ok(taskType);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"TaskType with ID {id} not found." });
        }
    }
}
