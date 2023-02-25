using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MMP.Core.Bootstrap.Configs;
using MMP.Core.Bootstrap.Services;

namespace MMP.Core.Data.Tests;

public class PostgresqlFixture : IDisposable
{
    private readonly TestcontainerDatabase _database;
    public readonly PostgresSql _config;
    
    public PostgresqlFixture()
    {
        _database = new ContainerBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "MMP_DB",
                Username = "admin",
                Password = "adminpass"
            })
            .Build();
        
        _database.StartAsync().GetAwaiter().GetResult();

        _config = new ();
        _config.Server = new();
        _config.Server.Host = _database.Hostname;
        _config.Server.Port = _database.Port.ToString();
        _config.Server.DatabaseName = "mmpcore_db";
        _config.Root = new();
        _config.Root.User = "admin";
        _config.Root.Password = "adminpass";
        _config.App = new();
        _config.App.User = "mmpcoreuser";
        _config.App.Password = "mmpcorepass";
        
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var bootstrapServiceLogger = loggerFactory.CreateLogger<PostgresSqlBootstrapService>();
        var fluentMigrationServiceLogger = loggerFactory.CreateLogger<FluentMigrationService>();
       
        var boostrapService = new PostgresSqlBootstrapService(bootstrapServiceLogger, _config); 
        boostrapService.ExecuteAsync().GetAwaiter().GetResult();

        var serviceProvider = CreateServices(GetConnectionString(_database));
        var fluentMigrationService = new FluentMigrationService(serviceProvider, fluentMigrationServiceLogger);
        fluentMigrationService.Run();
    }
    public void Dispose()
    {
        _database.DisposeAsync().AsTask().GetAwaiter().GetResult(); 
    }
    private ServiceProvider CreateServices(string connectionString) => new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres11_0()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(PostgresSqlBootstrapService).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    
    private static string GetConnectionString(TestcontainerDatabase database) => $"server={database.Hostname};Port={database.Port};Database=mmpcore_db;" +
                                                                          $"User Id={database.Username};Password={database.Password};";
    
    public string GetConnectionString() => $"server={_database.Hostname};Port={_database.Port};Database=mmpcore_db;" +
                                                                          $"User Id={_config.App.User};Password={_config.App.Password};";
}