using Xunit;
using Moq;
using AnimesProtech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AnimesProtech.Application.Controllers;
using AnimesProtech.Domain.Interfaces.UseCases;

namespace AnimesProtech.Tests.Controllers;
public class AnimesControllerTests
{
    [Fact]
    public async Task PostAnime_ReturnsCreatedAtActionResult_WithAnime()
    {
        // Arrange
        var mockUseCase = new Mock<IAddAnimeUseCase>();
        var anime = GetTestAnime();
        mockUseCase.Setup(useCase => useCase.Execute(It.IsAny<Anime>()))
            .ReturnsAsync(anime);

        var controller = new AnimesController(mockUseCase.Object);

        // Act
        var result = await controller.Post(anime);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var returnValue = Assert.IsType<Anime>(createdResult.Value);
        Assert.Equal(anime, returnValue);
    }

    private Anime GetTestAnime()
    {
        return new Anime()
        {
            name = "Test Anime",
            summary = "Test Summary",
            director = "Test Director"
        };
    }
}