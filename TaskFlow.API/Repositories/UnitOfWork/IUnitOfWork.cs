using TaskFlow.API.Repositories.Interfaces;

namespace TaskFlow.API.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        Task<int> SaveChangesAsync();
    }
}
