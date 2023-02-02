using Microsoft.EntityFrameworkCore;
using MMP.Core.Data.Entities;

namespace MMP.Core.Data.Repositories;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        :base(options)
    {
        
    }

    public DbSet<Operation> Operations { get; set; }
}