using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimesProtech.Application.Controllers
{
    [ApiController]
    [Route("/api/v1/animes")]
    [Authorize]
    public class AnimesController : ControllerBase
    {
        private readonly IAddAnimeUseCase _addAnimeUseCase;

        public AnimesController(IAddAnimeUseCase addAnimeUseCase)
        {
            _addAnimeUseCase = addAnimeUseCase;
        }

        [HttpPost]
        [AnimePostOperation]
        public async Task<IActionResult> Post(Anime anime)
        {
            var addedAnime = await _addAnimeUseCase.Execute(anime);
            return Created($"/api/v1/animes/{addedAnime.id}", addedAnime);
        }

    }
}