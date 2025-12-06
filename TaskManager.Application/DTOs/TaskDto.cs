namespace TaskManager.Application.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
    public Domain.Enums.TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public int TaskTypeId { get; set; }
    public string TaskTypeName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
