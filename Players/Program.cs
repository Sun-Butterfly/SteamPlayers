using Microsoft.EntityFrameworkCore;
using Players;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PlayersDataBaseContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<GameClient>(client =>
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("GameApi")!));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/all", async (PlayersDataBaseContext context, GameClient client) =>
{
    var players = await context.Players.ToListAsync();

    var allPlayerGameIds = players.SelectMany(x => x.GameIds);

    var allGames = await client.GetGamesByIds(allPlayerGameIds.ToList());
    
    var result = players.Select(player => new
    {
        player.Id,
        player.Name,
        Games = player.GameIds.Select(gameId => allGames.FirstOrDefault(game => game.Id == gameId))
    });
    return Results.Ok(result);
});

app.MapGet("/{id}", async (PlayersDataBaseContext context, GameClient client, long id) =>
{
    var player = await context.Players.Where(x => x.Id == id).FirstOrDefaultAsync();
    var gamesByPlayerId = await client.GetGamesByIds(player.GameIds);
    var result = new
    {
        player.Id,
        player.Name,
        Games = gamesByPlayerId
    };
    return Results.Ok(result);
});
app.Run();