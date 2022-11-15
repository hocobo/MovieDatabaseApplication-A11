using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Dao;
using MovieDatabaseApplication_A11.Dao;
using MovieDatabaseApplication_A11.Mappers;
using MovieDatabaseApplication_A11.Services;
using Spectre.Console;

namespace MovieDatabaseApplication_A11;

/// <summary>
///     Used for registration of new interfaces
/// </summary>
public class Startup
{
    public IServiceProvider ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddFile("app.log");
        });

        // Add new lines of code here to register any interfaces and concrete services you create
        services.AddTransient<IMainService, MainService>();
        services.AddTransient<IFileService, FileService>();
        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<IMovieMapper, MovieMapper>();
        services.AddDbContextFactory<MovieContext>();
        services.AddAutoMapper(typeof(MovieProfile));

        RegisterExceptionHandler();

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Used by Spectre.Console to "beautify" the exception output - this is not necessary if you are
    /// not using that specific library to create user menus - See Menu.cs for some fun examples
    /// </summary>
    public static void RegisterExceptionHandler()
    {
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            AnsiConsole.WriteException(eventArgs.Exception,
                ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes |
                ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
        };
    }
}
