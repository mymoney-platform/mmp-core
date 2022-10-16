using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMP.Core.Bootstrap.Configs;
using MMP.Core.Bootstrap.Services;

Console.WriteLine("Starting boostrap");
var host = CreateHostBuilder();
host.Build().Run();

Console.ReadKey();
Console.WriteLine("Finishing boostrap");

static IHostBuilder CreateHostBuilder(string[] args = null)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.json", optional: true);
            config.AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<IPostgresSqlBoostrapService>((sp) =>
            {
                var logger = sp.GetService<ILogger<PostgresSqlBoostrapService>>();
                var postgres = hostContext.Configuration.GetSection("PostgresSQL").Get<PostgresSql>();
                return new PostgresSqlBoostrapService(logger, postgres);
            });

            services.AddHostedService<Bootstrap>();
        })
        .ConfigureLogging((hostingContext, logging) =>
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
        });
}