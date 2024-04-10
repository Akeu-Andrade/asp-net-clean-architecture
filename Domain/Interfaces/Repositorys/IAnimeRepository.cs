using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces.Repositorys
{
    public interface IAnimeRepository : IBaseRepository<Anime>
    {
        Task<Anime?> GetByName(string name);
    }
}
