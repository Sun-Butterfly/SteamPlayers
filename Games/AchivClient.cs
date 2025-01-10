namespace Games;

public class AchivClient
{
    private readonly HttpClient _client;

    public AchivClient(HttpClient client)
    {
        _client = client;
    }


    public async Task<List<Achievement>> GetAchivsByGameId(long id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/game:{id}");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var achivs = await response.Content.ReadFromJsonAsync<List<Achievement>>();
        return achivs!;
    }

    public async Task<List<Achievement>> GetAchivsByGamesIds(List<long> gamesIds)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/many")
        {
            Content = JsonContent.Create(gamesIds)
        };
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var achivs = await response.Content.ReadFromJsonAsync<List<Achievement>>();
        return achivs;
    }
}

public record Achievement(long Id, string Name);
