using AnimesProtech.Application.UseCases;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces.Repositorys;
using Moq;
using Xunit;

namespace AnimesProtech.Tests.UseCases;
public class AddAnimeUseCaseTests
{
    [Fact]
    public async Task Execute_ReturnsAddedAnime()
    {
        // Arrange
        var mockRepository = new Mock<IAnimeRepository>();
        var anime = GetTestAnime();
        mockRepository.Setup(repo => repo.Add(It.IsAny<Anime>()))
            .ReturnsAsync(anime);

        var useCase = new AddAnimeUseCase(mockRepository.Object);

        // Act
        var result = await useCase.Execute(anime);

        // Assert
        Assert.Equal(anime, result);
    }

    private Anime GetTestAnime()
    {
        return new Anime()
        {
            name = "Test Anime usecase",
            summary = "Test Summary usecase",
            director = "Test Director usecase"
        };
    }
}