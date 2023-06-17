using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ReactiveUI;


namespace Root_Way.Repositories;

public class StorageRepository : RepositoryBase
{
    private readonly AppConfiguration _configuracion;
    public IEnumerable<String> GetScriptList(string username)
    {
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        return userContainer.GetBlobs().Select(blob => blob.Name);
    }

    public BlobContainerClient CheckAndCreateContainer(string username)
    {
        var connection = GetConnectionStorage();
        BlobContainerClient userClient = connection.GetBlobContainerClient(username);
        userClient.CreateIfNotExists();
        return userClient;
    }

    public bool DeleteFileFromCloud(string name, string username)
    {
        try
        {
            BlobContainerClient userContainer = CheckAndCreateContainer(username);
            userContainer.DeleteBlob(name);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    
    public bool DeleteFileFromLocal(string name)
    {
        try
        {
            File.Delete(_configuracion.scriptsDir + name);;
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool UploadFile(string name, string username)
    {
        if (!CheckLocalFile(name))
        {
            return false;
        }
        
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        
        try
        {
            userContainer.GetBlobClient(name).Upload(_configuracion.scriptsDir + name, true);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    
    public bool DownloadFile(string name, string username)
    {
        if (!CheckLocalFile(name))
        {
            return false;
        }
        
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        
        try
        {
            userContainer.GetBlobClient(name).Upload(_configuracion.scriptsDir + name, true);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    
    public bool CheckLocalFile(string name)
    {
        return File.Exists(_configuracion.scriptsDir + name);
    }
}