using System.Data;
using Npgsql;

namespace MMP.Core.Data.Repositories;

public class DatabaseFactory : IDatabaseFactory, IDisposable
{
    public IDbConnection DbConnection { get; init; }
    public DatabaseFactory(string connectionString)
    {
        DbConnection = new NpgsqlConnection(connectionString);
        DbConnection.Open();
    }
    public void Dispose()
    {
        DbConnection.Close();
        DbConnection.Dispose();
    }
}