using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Paginacion;

namespace TaskFlow.API.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDTO>> GetAll();
        Task<TaskItemDTO?> GetById(int id);
        Task<TaskItemDTO> Create(TaskItemInsertDTO task);
        Task<bool> Update(int id, TaskItemUpdateDTO task);
        Task<bool> Delete(int id);
        Task<IEnumerable<TaskItemDTO>> GetPaged(PaginationParams pagination);
    }
}
