namespace TaskManager.Application.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; } = Domain.Enums.TaskStatus.ToDo;
    public Domain.Enums.TaskPriority Priority { get; set; } = Domain.Enums.TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public int TaskTypeId { get; set; }
}
