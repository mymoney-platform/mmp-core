using Microsoft.Extensions.Logging;
using MMP.Core.Bootstrap.Configs;
using Npgsql;

namespace MMP.Core.Bootstrap.Services;

public class PostgresSqlBoostrapService : IPostgresSqlBoostrapService
{
    private readonly ILogger<PostgresSqlBoostrapService> _logger;
    private readonly PostgresSql _config;
    private readonly NpgsqlConnection _rootConnection;
    private NpgsqlConnection _dbConnection;

    public PostgresSqlBoostrapService(ILogger<PostgresSqlBoostrapService> logger, PostgresSql config)
    {
        _logger = logger;
        _config = config;

        _rootConnection = new NpgsqlConnection(BuildConnectionString("MMP_DB", config.Root));
        _rootConnection.Open();
    }

    public Task ExecuteAsync()
    {
        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} Starting... ");
        
        CreateAppUser();
        CreateDatabase();
        CreateDbConnection(_config);
        CreateSchema();
        SetPermissions();

        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} Finished successfully ");

        return Task.CompletedTask;
    }

    private void CreateAppUser()
    {
        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} CreateAppUser... ");

        using var command = _rootConnection.CreateCommand();

        command.CommandText = @$"SELECT count(rolname) FROM pg_catalog.pg_roles WHERE  rolname = '{_config.App.User}'";

        long qtd = (long)command.ExecuteScalar();

        if (qtd == 0)
        {
            _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} Creating user '{_config.App.User}'...");

            command.CommandText = $@"CREATE USER {_config.App.User} WITH PASSWORD '{_config.App.Password}'";

            command.CommandText = @$"
                    CREATE ROLE {_config.App.User} WITH
	                    LOGIN
	                    NOSUPERUSER
	                    NOCREATEDB
	                    NOCREATEROLE
	                    INHERIT
	                    NOREPLICATION
	                    CONNECTION LIMIT -1
	                    PASSWORD '{_config.App.User}'; ";

            command.ExecuteNonQuery();

            _logger.LogInformation(
                $"{nameof(PostgresSqlBoostrapService)} user '{_config.App.User}' created successfully");
        }
        else
        {
            _logger.LogInformation(
                $"{nameof(PostgresSqlBoostrapService)} user '{_config.App.User}' already exists, none user has been created");
        }
    }

    private void CreateDatabase()
    {
        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} CreateDatabase... ");

        using var command = _rootConnection.CreateCommand();

        command.CommandText =
            @$"SELECT count(datname) FROM pg_database WHERE datname = '{_config.Server.DatabaseName}'";

        long qtd = (long)command.ExecuteScalar();

        if (qtd == 0)
        {
            _logger.LogInformation(
                $"{nameof(PostgresSqlBoostrapService)} Creating DATABASE '{_config.Server.DatabaseName}'...");

            command.CommandText = @$"
                        CREATE DATABASE {_config.Server.DatabaseName} 
                            WITH 
                            OWNER = {_config.App.User}
                            ENCODING = 'UTF8'
                            CONNECTION LIMIT = -1; ";

            command.ExecuteNonQuery();

            _logger.LogInformation(
                $"{nameof(PostgresSqlBoostrapService)} DATABASE '{_config.Server.DatabaseName}' created successfully");
        }
        else
        {
            _logger.LogInformation(
                $"{nameof(PostgresSqlBoostrapService)} DATABASE '{_config.Server.DatabaseName}' already exists, none user has been created");
        }
    }

    private void SetPermissions()
    {
        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} SetPermissions... ");

        using var command = _dbConnection.CreateCommand();

        command.CommandText =
            $"GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA {_config.Server.DatabaseName} TO {_config.App.User};";

        command.ExecuteNonQuery();

        command.CommandText =
            $"GRANT UPDATE, USAGE, SELECT ON ALL SEQUENCES IN SCHEMA {_config.Server.DatabaseName} TO {_config.App.User};";

        command.ExecuteNonQuery();

        _logger.LogInformation(
            $"{nameof(PostgresSqlBoostrapService)} Grant permissions applied to the user '{_config.App.User}' to the schema '{_config.Server.DatabaseName}'");
    }

    private void CreateSchema()
    {
        _logger.LogInformation($"{nameof(PostgresSqlBoostrapService)} CreateSchema... ");

        using var command = _dbConnection.CreateCommand();

        command.CommandText = $"CREATE SCHEMA IF NOT EXISTS {_config.Server.DatabaseName};";

        command.ExecuteNonQuery();
        
        _logger.LogInformation(
            $"{nameof(PostgresSqlBoostrapService)} Created schema {_config.Server.DatabaseName}'");
    }

    private void CreateDbConnection(PostgresSql config)
    {
        _dbConnection = new NpgsqlConnection(BuildConnectionString($"{config.Server.DatabaseName}", config.Root));
        _dbConnection.Open();
    }

    private string BuildConnectionString(string database, Credential credential)
        =>
            $"server={_config.Server.Host};Port={_config.Server.Port};Database={database};User Id={credential.User};Password={credential.Password};";
}