using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MMP.Core.Bootstrap.Services;

public class FluentMigrationService : IFluentMigrationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FluentMigrationService> _logger;

    public FluentMigrationService(IServiceProvider serviceProvider, ILogger<FluentMigrationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    public void Run()
    {
        using var scope = _serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        _logger.Log(LogLevel.Information,$"Executing IMigrationRunner.MigrateUp()...");

        runner.MigrateUp();

        _logger.Log(LogLevel.Information,$"IMigrationRunner.MigrateUp() finished successfully!");
    }
}