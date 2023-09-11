using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Models;

namespace TaskManager;

public interface ISingleton {}
public interface ITransient {}
public interface IScoped {}

public static class IoC
{
    private static readonly IServiceProvider Provider;

    public static T Resolve<T>() where T : notnull => Provider.GetRequiredService<T>();
    
    static IoC()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        
        var services = new ServiceCollection();
        
        services.AddDbContext<TaskManageContext>(s 
            => s.UseSqlite(configuration.GetConnectionString("sqlite")));
        
        services.Scan(
            s => s.FromAssemblyOf<ITransient>()
                .AddClasses(x => x.AssignableTo<ITransient>()).AsSelf().WithTransientLifetime()
                .AddClasses(x => x.AssignableTo<IScoped>()).AsSelf().WithScopedLifetime()
                .AddClasses(x => x.AssignableTo<ISingleton>()).AsSelf().WithSingletonLifetime()
            );

        Provider = services.BuildServiceProvider();

        foreach (var service in services.Where(s => s.Lifetime == ServiceLifetime.Singleton))
            Provider.GetRequiredService(service.ServiceType);
    }
}