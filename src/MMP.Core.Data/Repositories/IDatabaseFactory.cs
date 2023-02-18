using System.Data;

namespace MMP.Core.Data.Repositories;

public interface IDatabaseFactory
{
    public IDbConnection DbConnection { get; init; }
}