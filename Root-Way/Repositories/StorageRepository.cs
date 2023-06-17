using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ReactiveUI;
using Root_Way.Models;


namespace Root_Way.Repositories;

public class StorageRepository : RepositoryBase
{
    // --------------------------------------------------------
    // Get data 
    // --------------------------------------------------------
    public IEnumerable<String> GetScriptCloudList(string username)
    {
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        return userContainer.GetBlobs().Select(blob => blob.Name);
    }

    public IEnumerable<String> GetScriptLocalList()
    {
        return Directory.EnumerateFiles(configuration["scriptsDir"]);
    }

    public IEnumerable<FileModel> GetAllScripts(string username)
    {
        IEnumerable<FileModel> cloudScripts = GetScriptCloudList(username).Select(file => new FileModel(){Name = file, FileType = FileType.Cloud});
        IEnumerable<FileModel> localScripts = GetScriptLocalList().Select(file => new FileModel(){Name = file, FileType = FileType.Local});
        
        IEnumerable<FileModel> mergedScripts = cloudScripts.Union(localScripts);

        foreach (FileModel file in mergedScripts)
        {
            if (file.FileType == FileType.Cloud && localScripts.Any(f => f.Name == file.Name))
            {
                file.FileType = FileType.Both;
            }
            else if (file.FileType == FileType.Local && cloudScripts.Any(f => f.Name == file.Name))
            {
                file.FileType = FileType.Both;
            }
        }

        return mergedScripts;
    }
    
    public bool CheckLocalFile(string name)
    {
        return File.Exists(configuration["scriptsDir"] + name);
    }

    public BlobContainerClient CheckAndCreateContainer(string username)
    {
        var connection = GetConnectionStorage();
        BlobContainerClient userClient = connection.GetBlobContainerClient(username);
        userClient.CreateIfNotExists();
        return userClient;
    }
    
    // --------------------------------------------------------
    // Delete data 
    // --------------------------------------------------------

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
            File.Delete(configuration["scriptsDir"] + name);;
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    // --------------------------------------------------------
    // Post data 
    // --------------------------------------------------------

    public bool UploadFile(string name, string username)
    {
        if (!CheckLocalFile(name))
        {
            return false;
        }
        
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        
        try
        {
            userContainer.GetBlobClient(name).Upload(configuration["scriptsDir"] + name, true);
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
            userContainer.GetBlobClient(name).Upload(configuration["scriptsDir"] + name, true);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
}