using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Mysqlx.Datatypes;
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
        return Directory.EnumerateFiles(AppConfiguration.GetValue("scriptsDir")).Select(file => file.Split("/").Last());
    }

    public IEnumerable<FileModel> GetAllScripts(string username)
    {
        IEnumerable<FileModel> cloudScripts = GetScriptCloudList(username).Select(file => new FileModel(){Name = file, FileType = FileType.Cloud});
        IEnumerable<FileModel> localScripts = GetScriptLocalList().Select(file => new FileModel(){Name = file, FileType = FileType.Local});
        
        IEnumerable<FileModel> mergedScripts = cloudScripts.Concat(localScripts);
        
        return mergedScripts.GroupBy(file => file.Name)
            .Select(group =>
            {
                FileType newFileType = group.Count() > 1 ? FileType.Both : group.First().FileType;
                return new FileModel { Name = group.Key, FileType = newFileType };
            });;
    }
    
    public bool CheckLocalFile(string name)
    {
        return File.Exists(AppConfiguration.GetValue("scriptsDir") + name);
    }
    
    public bool CheckCloudFile(string name, string username)
    {
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        return userContainer.GetBlobClient(name).Exists();
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
        if (!CheckCloudFile(name, username))
        {
            return false;
        }
        
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
        if (!CheckLocalFile(name))
        {
            return false;
        }
        
        try
        {
            File.Delete(AppConfiguration.GetValue("scriptsDir") + name);;
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
            userContainer.GetBlobClient(name).Upload(AppConfiguration.GetValue("scriptsDir") + name, true);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    
    public bool DownloadFile(string name, string username)
    {
        if (!CheckCloudFile(name, username))
        {
            return false;
        }
        
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        
        try
        {
            DeleteFileFromLocal(name);
            
            using (FileStream writeStream = File.Create(AppConfiguration.GetValue("scriptsDir") + name))
            {
                userContainer.GetBlobClient(name).DownloadTo(writeStream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }

        return true;
    }
}