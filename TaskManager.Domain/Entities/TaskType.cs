using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities;

public class TaskType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(7)]
    public string? ColorCode { get; set; } // HEX цвет для UI
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Навигационное свойство
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
