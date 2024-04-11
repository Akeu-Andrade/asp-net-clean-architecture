using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Domain.Specifications;

namespace AnimesProtech.Application.UseCases
{
    public class DeleteAnimeUseCase : IDeleteAnimeUseCase
    {
        private readonly IAnimeRepository _animeRepository;

        public DeleteAnimeUseCase(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task Execute(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            await _animeRepository.Delete(id);

        }
    }
}