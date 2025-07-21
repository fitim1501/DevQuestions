using System.Data;

namespace DevQuestions.Application.DataBase;

public interface ISqlConnectionFactory
{
    IDbConnection Creat();
}