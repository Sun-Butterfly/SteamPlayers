using Achievements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AchivDataBaseContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/game:{id}", async (AchivDataBaseContext context, long id) =>
{
    var achivs = await context.Achievements.Where(x => x.GameId == id).ToListAsync();
    return Results.Ok(achivs);
});

app.MapPost("/many", async (AchivDataBaseContext context, [FromBody] List<long> gameIds) =>
{
    var achivs = await context.Achievements.Where(x => gameIds.Contains(x.GameId)).ToListAsync();
    return Results.Ok(achivs);
});

app.Run();