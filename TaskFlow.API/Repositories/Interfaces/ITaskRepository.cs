using TaskFlow.API.Models;

namespace TaskFlow.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem?> GetById(int id);
        Task<TaskItem> Create(TaskItem task);
        Task<bool> Update(TaskItem task);
        Task<bool> Delete(TaskItem task);
    }
}
