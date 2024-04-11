using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Specifications;

namespace AnimesProtech.Domain.Interfaces.Repositorys
{
    public interface IAnimeRepository : IBaseRepository<Anime>
    {
        Task<List<Anime>> GetAll(AnimeSearchCriteria criteria);
    }
}
