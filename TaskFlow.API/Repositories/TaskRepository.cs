using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.Models;
using TaskFlow.API.Repositories.Interfaces;

namespace TaskFlow.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskFlowDbContext _context;
        public TaskRepository(TaskFlowDbContext context)
        {
            _context = context;
        }
        //Se agrega AsNoTracking() para mejorar el rendimiento al obtener los datos, ya que no se necesita realizar un seguimiento de
        //los cambios en las entidades recuperadas. Esto es especialmente útil cuando solo se necesitan leer los datos sin modificarlos.
        public async Task<IEnumerable<TaskItem>> GetAll()
        => await _context.Tasks.AsNoTracking().ToListAsync();

        public async Task<TaskItem?> GetById(int id)
         => await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<TaskItem> Create(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<bool> Update(TaskItem task)
        {
            _context.Tasks.Update(task);
            return await _context.SaveChangesAsync() > 0; //Retorna true si se guardaron los cambios, false si no se guardaron
        }

        public async Task<bool> Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync() > 0; //Retorna true si se guardaron los cambios, false si no se guardaron
        }

        public async Task<IEnumerable<TaskItem>> GetPaged(int page, int pageSize)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
