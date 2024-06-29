using System.ComponentModel.DataAnnotations;

namespace kt6.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }

    public enum TaskStatus
    {
        New,
        InProgress,
        Completed
    }
}
