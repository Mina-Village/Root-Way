using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public abstract class RepositoryBase
{
    private readonly string _connectionString;
    public RepositoryBase()
    {
        _connectionString = "Server=proyecto-final2023.mysql.database.azure.com;UserID=admina;Password=PINKsky-2001;Database=RootWay;SslMode=Required;";
    }
    protected MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}