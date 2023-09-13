using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Models;
using TaskManager.Services.Implementation;
using TaskManager.Services.Interface;
using TaskManager.ViewModels;

namespace TaskManager;

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

        // Services
        services.AddScoped<IPageService, PageService>();
        services.AddSingleton<ISynchronizeService, SynchronizeService>();

        // ViewModels
        services.AddTransient<MainViewModel>();
        
        Provider = services.BuildServiceProvider();

        foreach (var service in services.Where(s => s.Lifetime == ServiceLifetime.Singleton))
            Provider.GetRequiredService(service.ServiceType);
    }
}