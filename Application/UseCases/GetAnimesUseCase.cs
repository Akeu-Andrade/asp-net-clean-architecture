using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Domain.Specifications;

namespace AnimesProtech.Application.UseCases
{
    public class GetAnimesUseCase : IGetAnimesUseCase
    {
        private readonly IAnimeRepository _animeRepository;

        public GetAnimesUseCase(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<List<Anime>> Execute(AnimeSearchCriteria criteria)
        {
            return await _animeRepository.GetAll(criteria);
        }
    }
}