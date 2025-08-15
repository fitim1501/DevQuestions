using System.Data;

namespace Shared.DataBase;

public interface ISqlConnectionFactory
{
    IDbConnection Creat();
}