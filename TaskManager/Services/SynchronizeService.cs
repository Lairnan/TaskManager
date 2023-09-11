using System.Threading.Tasks;
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
}