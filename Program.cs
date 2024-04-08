using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces;
using AnimesProtech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API PROTECH", Version = "v1" });
});

builder.Services.AddTransient<IAnimeRepository, AnimeRepository>();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        mySqlConnectionStr, 
        ServerVersion.AutoDetect(mySqlConnectionStr)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/animes", async (Anime anime, IAnimeRepository _animeRepository) =>
{
    _animeRepository.Add(anime);
    return Results.Created($"/api/v1/animes/{anime.id}", anime);
})
    .WithName("Adiciona Novo Anime")
    .WithOpenApi(it => new Microsoft.OpenApi.Models.OpenApiOperation(it)
    {
        Description = "Adiciona um novo anime",
        OperationId = "AdicionaNovoAnime",
        Tags = new[] { new OpenApiTag { Name = "Animes" } }
    });

app.Run();
