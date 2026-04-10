using TaskFlow.API.Data;
using TaskFlow.API.Repositories.Interfaces;

namespace TaskFlow.API.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskFlowDbContext _context;
        public ITaskRepository Tasks { get; }

        public UnitOfWork(TaskFlowDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
