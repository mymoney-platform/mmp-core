using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMP.Core.Bootstrap.Services;

public class Bootstrap : IHostedService
{
    private readonly ILogger<Bootstrap> _logger;
    private readonly IPostgresSqlBoostrapService _postgresSqlBoostrapService;

    public Bootstrap(ILogger<Bootstrap> logger, IPostgresSqlBoostrapService postgresSqlBoostrapService)
    {
        _logger = logger;
        _postgresSqlBoostrapService = postgresSqlBoostrapService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting boostrap");
        await _postgresSqlBoostrapService.ExecuteAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shutting down boostrap");
    }
}