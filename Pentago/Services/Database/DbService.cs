using System.Data.Common;
using System.Data.SQLite;

namespace Pentago.Services.Database;

public class DbService : IDbService
{
    private readonly string _connectionString;
    
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("App");
    }
    
    public DbConnection GetDbConnection()
    {
        return new SQLiteConnection(_connectionString);
    }
}