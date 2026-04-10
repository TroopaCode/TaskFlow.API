using TaskFlow.API.Models;
using TaskFlow.API.Paginacion;

namespace TaskFlow.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem?> GetById(int id);
        Task<TaskItem> Create(TaskItem task);
        Task<bool> Update(TaskItem task);
        Task<bool> Delete(TaskItem task);

        //Agrega un nuevo método para obtener tareas paginadas
        Task<IEnumerable<TaskItem>> GetPaged(int page, int pageSize);
    }
}
