using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.Models;
using TaskFlow.API.Paginacion;
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
         => await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.TaskItemId == id);

        public async Task<TaskItem> Create(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            //No se llama a SaveChangesAsync() aquí porque el patrón Unit of Work se encarga
            //de llamar a este método después de realizar todas las operaciones necesarias en el repositorio.
            //await _context.SaveChangesAsync(); 
            return task;
        }
        public async Task<bool> Update(TaskItem task)
        {
            _context.Tasks.Update(task);
            return true; 
        }

        public async Task<bool> Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return true; 
        }

        //Agrega paginación a la consulta para obtener solo un subconjunto de tareas según la página y el tamaño de página especificados
        public async Task<IEnumerable<TaskItem>> GetPaged(int page, int pageSize) 
        {
            //Retorna una lista de tareas paginada, omitiendo las tareas de las páginas anteriores y tomando solo las tareas de la página
            //actual según el tamaño de página especificado.
            return await _context.Tasks
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); //Skip() omite un número específico de elementos y Take() toma un número específico de elementos después de omitir
        }

        public async Task<PagedResult<TaskItem>> GetPaged(PaginationParams parameters)
        {
            var query = _context.Tasks.AsNoTracking().AsQueryable();

            // 🔍 FILTRO por estado
            if (parameters.IsCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == parameters.IsCompleted.Value);
            }

            // 🔍 FILTRO por búsqueda
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(t => t.Title.Contains(parameters.Search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }
    }
}
