using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Models;

namespace TaskManager.Services;

public static class SynchronizeService
{
    public static async Task SendObjectAsync<T>(this T obj)
        where T : IDbEntity
    {
    }
    
    public static async Task<T?> ReceiveObjectAsync<T>(this T obj)
        where T : IDbEntity
    {
        return default;
    }

    public static async Task<IpInfo?> GetCurrentIpAsync()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://ipinfo.io/json");

        if (!response.IsSuccessStatusCode) return default;
        
        var jsonContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IpInfo>(jsonContent);


    }
    
    
}