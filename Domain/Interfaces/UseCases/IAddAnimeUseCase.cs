using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces.UseCases
{
    public interface IAddAnimeUseCase
    {
        Task<Anime> Execute(Anime anime);
    }
}
