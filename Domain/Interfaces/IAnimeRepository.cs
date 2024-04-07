using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces
{
    public interface IAnimeRepository : IBaseRepository<Anime>
    {
        Task<Anime?> GetByName(string name, CancellationToken cancellationToken);
    }
}
