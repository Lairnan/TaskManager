using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services.Interface;

public interface ISynchronizeService
{
    Task SendObjectAsync<T>(T obj) where T : IDbEntity;
    Task<T?> ReceiveObjectAsync<T>(T obj) where T : IDbEntity;
    Task<IpInfo?> GetCurrentIpAsync();
}