using Xunit;
using Moq;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AnimesProtech.Domain.Interfaces.DbContext;

namespace AnimesProtech.Tests.Repositorys;
public class AnimeRepositoryTests
{
    [Fact]
    public async Task Add_AddsAnimeToDatabase()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Anime>>();
        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(m => m.Animes).Returns(mockSet.Object);

        var repository = new AnimeRepository(mockContext.Object);
        var anime = GetTestAnime();

        // Act
        await repository.Add(anime);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<Anime>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
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