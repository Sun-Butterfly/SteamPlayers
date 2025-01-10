using Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GamesDataBaseContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/all", async (GamesDataBaseContext context) =>
{
    var games = await context.Games.ToListAsync();
    return Results.Ok(games);
});

app.MapGet("/{id}", async (GamesDataBaseContext context, long id) =>
{
    var game = await context.Games.Where(x => x.Id == id).FirstOrDefaultAsync();
    return Results.Ok(game);
});

app.MapPost("/many", async (GamesDataBaseContext context, [FromBody] List<long> ids) =>
{
    var games = await context.Games.Where(x => ids.Contains(x.Id)).ToListAsync();
    return Results.Ok(games);
});

app.Run();