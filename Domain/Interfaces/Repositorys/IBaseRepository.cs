using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces.Repositorys
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
