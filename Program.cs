using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interfaces;
using AnimesProtech.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
    .ExecuteAsync(statusCodeContext.HttpContext);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/v1/identity/").MapIdentityApi<IdentityUser>();

app.MapPost("/api/v1/animes", async (Anime anime, IAnimeRepository _animeRepository) =>
{
    await _animeRepository.Add(anime);
    return Results.Created($"/api/v1/animes/{anime.id}", anime);
})
    .WithName("Adiciona Novo Anime")
    .RequireAuthorization()
    .WithOpenApi(it => new Microsoft.OpenApi.Models.OpenApiOperation(it)
    {
        Description = "Adiciona um novo anime",
        OperationId = "AdicionaNovoAnime",
        Tags = new[] { new OpenApiTag { Name = "Animes" } }
    });

app.Run();
