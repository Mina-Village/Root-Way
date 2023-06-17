using Azure.Storage.Blobs;
using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public abstract class RepositoryBase
{
    private readonly string _connectionStringDB;
    private readonly string _connectionStringStorage;
    public RepositoryBase()
    {

        _connectionStringDB = AppConfiguration.GetValue("connectionStringDB");;
        _connectionStringStorage = AppConfiguration.GetValue("connectionStringStorage");
    }
    protected MySqlConnection GetConnectionDB()
    {
        return new MySqlConnection(_connectionStringDB);
    }

    protected BlobServiceClient GetConnectionStorage()
    {
        return new BlobServiceClient(_connectionStringStorage);
    }
}