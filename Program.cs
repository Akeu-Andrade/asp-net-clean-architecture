using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.Application.UseCases;
using AnimesProtech.Domain.Interfaces.DbContext;
using AnimesProtech.Domain.Interfaces.Repositorys;
using AnimesProtech.Domain.Interfaces.UseCases;
using AnimesProtech.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigSwagger();

builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddTransient<IAnimeRepository, AnimeRepository>();
builder.Services.AddTransient<IAddAnimeUseCase, AddAnimeUseCase>();
builder.Services.AddTransient<IGetAnimesUseCase, GetAnimesUseCase>();
builder.Services.AddTransient<IUpdateAnimeUseCase, UpdateAnimeUseCase>();
builder.Services.AddTransient<IDeleteAnimeUseCase, DeleteAnimeUseCase>();

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

builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
    .ExecuteAsync(statusCodeContext.HttpContext);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapGroup("/api/v1/identity/").MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();