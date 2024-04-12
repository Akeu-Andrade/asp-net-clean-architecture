using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Interfaces.UseCases
{
    public interface IUpdateAnimeUseCase
    {
        Task<Anime> Execute(Anime anime);
    }
}
