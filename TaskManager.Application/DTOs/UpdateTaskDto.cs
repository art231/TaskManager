namespace TaskManager.Application.DTOs;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
    public Domain.Enums.TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public int TaskTypeId { get; set; }
}
