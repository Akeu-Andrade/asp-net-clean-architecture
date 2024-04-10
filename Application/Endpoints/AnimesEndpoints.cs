using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;

namespace AnimesProtech.Application.Endpoints
{
    public static class AnimesEndpoints
    {
        public static void RegisterAnimesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/v1/animes").WithTags(nameof(Anime));

            group.MapPost("/", async (Anime anime, IAddAnimeUseCase addAnimeUseCase) =>
            {
                var addedAnime = await addAnimeUseCase.Execute(anime);
                return Results.Created($"/api/v1/animes/{addedAnime.id}", addedAnime);
            })
                .WithName("Adiciona Novo Anime")
                .RequireAuthorization()
                .WithOpenApi(OpenApiConfigurations.AnimePostOperation);
        }

    }
}
