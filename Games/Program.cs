using Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GamesDataBaseContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<AchivClient>(client =>
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("AchievementApi")!));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/all", async (GamesDataBaseContext context, AchivClient client) =>
{
    var games = await context.Games.ToListAsync();
    var gamesIds = games.Select(x => x.Id).ToList();
    var achivsByGamesIds = await client.GetAchivsByGamesIds(gamesIds);

    var result = games.Select(game => new
    {
        game.Id,
        game.Name,
        Achievements = game.AchievementIds.Select(achievementId =>
            achivsByGamesIds.FirstOrDefault(achievement => achievement.Id == achievementId))
    });
    return Results.Ok(result);
});

app.MapGet("/{id}", async (GamesDataBaseContext context, AchivClient client, long id) =>
{
    var game = await context.Games.Where(x => x.Id == id).FirstOrDefaultAsync();
    var achivs = await client.GetAchivsByGameId(id);
    var result = new
    {
        game.Id,
        game.Name,
        Achievements = achivs
    };

    return Results.Ok(result);
});

app.MapPost("/many", async (GamesDataBaseContext context, [FromBody] List<long> ids) =>
{
    var games = await context.Games.Where(x => ids.Contains(x.Id)).ToListAsync();
    return Results.Ok(games);
});

app.Run();