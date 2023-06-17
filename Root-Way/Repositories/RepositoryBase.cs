using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public abstract class RepositoryBase
{
    private readonly string _connectionString;
    public RepositoryBase()
    {
        _connectionString = AppConfiguration.GetValue("connectionString");
    }
    protected MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}