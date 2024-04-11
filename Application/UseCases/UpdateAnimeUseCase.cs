using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Domain.Specifications;

namespace AnimesProtech.Application.UseCases
{
    public class UpdateAnimeUseCase : IUpdateAnimeUseCase
    {
        private readonly IAnimeRepository _animeRepository;

        public UpdateAnimeUseCase(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<Anime> Execute(Anime anime)
        {
            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime));
            }

            return await _animeRepository.Update(anime);
        }
    }
}