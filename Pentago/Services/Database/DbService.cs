using System.Data.Common;
using Microsoft.Data.Sqlite;

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
        return new SqliteConnection(_connectionString);
    }
}