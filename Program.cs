using AnimesProtech.Application.ConfigDoument;
using AnimesProtech.ProjectExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigSwagger();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Connection string not found");

builder.Services
    .AddApplicationServices()
    .AddDatabase(mySqlConnectionStr)
    .AddIdentityServices()
    .AddLoggingServices();

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