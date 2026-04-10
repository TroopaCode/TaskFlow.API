using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.API.Models
{
    public class TaskItem
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskItemId { get; set; }

        //[Required]
        //[MaxLength(30)]
        public string? Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
