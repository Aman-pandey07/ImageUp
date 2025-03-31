namespace TaskManagementSystem.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public required string Title { get; set; } 
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
