namespace AnimesProtech.Domain.Interfaces.UseCases
{
    public interface IDeleteAnimeUseCase
    {
        Task Execute(Guid id);
    }
}
