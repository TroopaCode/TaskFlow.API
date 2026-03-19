using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Models;

namespace TaskFlow.API.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        { }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
