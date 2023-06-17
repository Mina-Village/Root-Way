using Azure.Storage.Blobs;
using MySql.Data.MySqlClient;

namespace Root_Way.Repositories;

public abstract class RepositoryBase
{
    private readonly string _connectionStringDB;
    private readonly string _connectionStringStorage;
    public RepositoryBase()
    {

        _connectionStringDB = "Server=proyecto-final2023.mysql.database.azure.com;UserID=admina;Password=PINKsky-2001;Database=RootWay;SslMode=Required;";
        _connectionStringStorage =
            "DefaultEndpointsProtocol=https;AccountName=rootwayscripts;AccountKey=6SS3MYHovGyf2tFviCw3/Xub1iyrmbFJAP9D6sF/BHL9y/X+uY/KfQ9y0iYs4QSptgAjETB8EJfq+AStJrdhWw==;EndpointSuffix=core.windows.net";
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