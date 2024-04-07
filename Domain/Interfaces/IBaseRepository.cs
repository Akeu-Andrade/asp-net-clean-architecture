using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetById(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
