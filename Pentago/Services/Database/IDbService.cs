using System.Data.Common;

namespace Pentago.Services.Database;

public interface IDbService
{
    DbConnection GetDbConnection();
}