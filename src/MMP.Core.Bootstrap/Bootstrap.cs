using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMP.Core.Bootstrap.Services;

public class Bootstrap : IHostedService
{
    private readonly ILogger<Bootstrap> _logger;
    private readonly IPostgresSqlBoostrapService _postgresSqlBoostrapService;
    private readonly IFluentMigrationService _fluentMigrationService;

    public Bootstrap(ILogger<Bootstrap> logger, IPostgresSqlBoostrapService postgresSqlBoostrapService, 
        IFluentMigrationService fluentMigrationService)
    {
        _logger = logger;
        _postgresSqlBoostrapService = postgresSqlBoostrapService;
        _fluentMigrationService = fluentMigrationService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting boostrap");
        await _postgresSqlBoostrapService.ExecuteAsync();
        _fluentMigrationService.Run();
        
        await StartAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shutting down boostrap");
    }
}