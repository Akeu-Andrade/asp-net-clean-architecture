using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces;

namespace AnimesProtech.Application.Endpoints
{
    public static class AnimesEndpoints
    {
        public static void RegisterAnimesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/v1/animes").WithTags(nameof(Anime));

            group.MapPost("/", async (Anime anime, IAnimeRepository _animeRepository) =>
            {
                await _animeRepository.Add(anime);

                return Results.Created($"{anime.id}", anime);
            })
                .WithName("Adiciona Novo Anime")
                .RequireAuthorization()
                .WithOpenApi(OpenApiConfigurations.AnimePostOperation);
        }

    }
}
