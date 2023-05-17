using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public abstract class RepositoryBase
{
    private readonly string _connectionString;
    public RepositoryBase()
    {
        _connectionString = "Server=myServerAddress;Database=myDatabase;Uid=myUsername;Pwd=myPassword;";
    }
    protected MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}