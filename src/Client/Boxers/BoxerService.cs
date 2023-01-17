using Client.Extensions;
using Client.Infrastructure;
using System.Net.Http.Json;
using VirtuaBoxer.Shared.Boxers;

namespace VirtuaBoxer.Client.Boxers;

public class BoxerService : IBoxerService
{
    private readonly HttpClient authenticatedClient;
    private readonly PublicClient publicClient;
    private const string endpoint = "boxer";

    public BoxerService(HttpClient authenticatedClient, PublicClient publicClient)
    {
        this.authenticatedClient = authenticatedClient;
        this.publicClient = publicClient;
    }
    public async Task<int> CreateAsync(BoxerDto.Mutate model)
    {
        var response = await authenticatedClient.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }
    public async Task DeleteAsync(int boxerId)
    {
        await authenticatedClient.DeleteAsync($"{endpoint}/{boxerId}");
    }

    public async Task EditAsync(int boxerId, BoxerDto.Mutate model)
    {
        await authenticatedClient.PutAsJsonAsync($"{endpoint}/{boxerId}", model);
    }

    public async Task<BoxerDto.Detail> GetDetailAsync(int boxerId)
    {
        return await publicClient.Client.GetFromJsonAsync<BoxerDto.Detail>($"{endpoint}/{boxerId}");
    }

    public async Task<BoxerResponse.GetIndex> GetIndexAsync(BoxerRequest.GetIndex request)
    {
        var queryparams = request.GetQueryString();
        return await publicClient.Client.GetFromJsonAsync<BoxerResponse.GetIndex>($"{endpoint}?{queryparams}");
    }
}
