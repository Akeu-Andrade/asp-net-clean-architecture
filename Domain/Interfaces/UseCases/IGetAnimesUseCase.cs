using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Specifications;

namespace AnimesProtech.Domain.Interfaces.UseCases
{
    public interface IGetAnimesUseCase
    {
        Task<List<Anime>> Execute(AnimeSearchCriteria criteria);
    }
}