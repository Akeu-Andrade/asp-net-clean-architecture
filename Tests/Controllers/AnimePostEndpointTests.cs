using AnimesProtech.Application.Controllers;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AnimesProtech.Application.Tests.Controllers;
public class AnimesControllerTests
{
    private readonly Mock<IAddAnimeUseCase> mockAddAnimeUseCase;
    private readonly Mock<IGetAnimesUseCase> mockGetAnimesUseCase;
    private readonly Mock<IUpdateAnimeUseCase> mockUpdateAnimeUseCase;
    private readonly Mock<IDeleteAnimeUseCase> mockDeleteAnimeUseCase;

    public AnimesControllerTests()
    {
        mockAddAnimeUseCase = new Mock<IAddAnimeUseCase>();
        mockGetAnimesUseCase = new Mock<IGetAnimesUseCase>();
        mockUpdateAnimeUseCase = new Mock<IUpdateAnimeUseCase>();
        mockDeleteAnimeUseCase = new Mock<IDeleteAnimeUseCase>();
    }

    [Fact]
    public async Task PostAnime_ReturnsCreatedAtActionResult_WithAnime()
    {
        // Arrange
        var anime = GetTestAnime();
        mockAddAnimeUseCase.Setup(useCase => useCase.Execute(It.IsAny<Anime>()))
            .ReturnsAsync(anime);

        var controller = new AnimesController(
            mockAddAnimeUseCase.Object, 
            mockGetAnimesUseCase.Object,
            mockUpdateAnimeUseCase.Object,
            mockDeleteAnimeUseCase.Object
        );

        // Act
        var result = await controller.Post(anime);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var returnValue = Assert.IsType<Anime>(createdResult.Value);
        Assert.Equal(anime, returnValue);
    }

    [Fact]
    public async Task GetAnimes_ReturnsOkObjectResult_WithListOfAnimes()
    {
        // Arrange
        var animes = GetTestAnimes();
        var criteria = new AnimeSearchCriteria();
        mockGetAnimesUseCase.Setup(useCase => useCase.Execute(It.IsAny<AnimeSearchCriteria>()))
            .ReturnsAsync(animes);

        var controller = new AnimesController(
            mockAddAnimeUseCase.Object, 
            mockGetAnimesUseCase.Object,
            mockUpdateAnimeUseCase.Object,
            mockDeleteAnimeUseCase.Object 
        );

        // Act
        var result = await controller.Get(criteria);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Anime>>(okResult.Value);
        Assert.Equal(animes, returnValue);
    }

    private List<Anime> GetTestAnimes()
    {
        return new List<Anime>()
        {
            GetTestAnime(),
            GetTestAnime(),
            GetTestAnime()
        };
    }

    private Anime GetTestAnime()
    {
        var random = new Random().Next(1, 20);
        return new Anime()
        {
            name = $"Test Anime {random}",
            summary = $"Test Summary {random}",
            director = $"Test Director {random}"
        };
    }

}