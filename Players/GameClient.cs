namespace Players;

public class GameClient
{
    private readonly HttpClient _client;

    public GameClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Game> GetGameById(long id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/{id}");
        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var game = await response.Content.ReadFromJsonAsync<Game>();
        return game!;
    }

    public async Task<List<Game>> GetAllGames()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/all");
        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var games = await response.Content.ReadFromJsonAsync<List<Game>>();
        return games!;
    }
    
    public async Task<List<Game>> GetGamesByIds(List<long> ids)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/many")
        {
            Content = JsonContent.Create(ids)
        };
        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var games = await response.Content.ReadFromJsonAsync<List<Game>>();
        return games!;
    }
}

public record Game(long Id, string Name);