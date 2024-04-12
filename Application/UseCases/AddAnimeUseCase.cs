using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;

namespace AnimesProtech.Application.UseCases
{
    public class AddAnimeUseCase : IAddAnimeUseCase
    {
        private readonly IAnimeRepository _animeRepository;

        public AddAnimeUseCase(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<Anime> Execute(Anime anime)
        {
            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime));
            }

            var existingAnime = await _animeRepository.GetByName(anime.name);
            if (existingAnime != null)
            {
                throw new InvalidOperationException("Já existe um anime com o mesmo nome");
            }

            return await _animeRepository.Add(anime);
        }
    }
}