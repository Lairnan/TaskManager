using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Models;
using TaskManager.Services.Interface;

namespace TaskManager.Services.Implementation;

public class SynchronizeService : ISynchronizeService
{
    public async Task SendObjectAsync<T>(T obj)
        where T : IDbEntity
    {
    }

    public async Task<T?> ReceiveObjectAsync<T>(T obj)
        where T : IDbEntity
    {
        return default;
    }

    public async Task<IpInfo?> GetCurrentIpAsync()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://ipinfo.io/json");

        if (!response.IsSuccessStatusCode) return default;

        var jsonContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IpInfo>(jsonContent);
    }
}