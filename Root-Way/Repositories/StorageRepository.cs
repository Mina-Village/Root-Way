using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // Obtiene la lista de scripts en la nube para el usuario especificado.
        // Verifica y crea el contenedor correspondiente al usuario.
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        // Devuelve los nombres de los blobs en el contenedor.
        return userContainer.GetBlobs().Select(blob => blob.Name);
    }

    public IEnumerable<String> GetScriptLocalList()
    {
        // Obtiene la lista de scripts locales.
        // Utiliza el directorio de scripts especificado en la configuración de la aplicación.
        return Directory.EnumerateFiles(AppConfiguration.GetValue("scriptsDir")).Select(file => file.Split("/").Last());
    }

    public ObservableCollection<FileModel> GetAllScripts(string username)
    {
        // Obtiene todos los scripts, tanto en la nube como locales, para el usuario especificado.
        IEnumerable<FileModel> cloudScripts = GetScriptCloudList(username).Select(file => new FileModel(){Name = file, FileType = FileType.Cloud});
        IEnumerable<FileModel> localScripts = GetScriptLocalList().Select(file => new FileModel(){Name = file, FileType = FileType.Local});
        
        // Combina las listas de scripts en la nube y locales.
        IEnumerable<FileModel> mergedScripts = cloudScripts.Concat(localScripts);
        
        // Agrupa los scripts por nombre y crea una nueva colección observable de modelos de archivos.
        return new ObservableCollection<FileModel>(mergedScripts.GroupBy(file => file.Name)
            .Select(group =>
            {
                // Si hay más de un script con el mismo nombre, se marca como FileType.Both (ambos).
                FileType newFileType = group.Count() > 1 ? FileType.Both : group.First().FileType;
                // Crea un nuevo modelo de archivo con el nombre y el tipo de archivo determinados.
                return new FileModel { Name = group.Key, FileType = newFileType };
            }));
    }
    
    public bool CheckLocalFile(string name)
    {
        // Verifica si el archivo local existe en el directorio especificado.
        return File.Exists(AppConfiguration.GetValue("scriptsDir") + name);
    }
    
    public bool CheckCloudFile(string name, string username)
    {
        // Verifica si el archivo en la nube existe en el contenedor del usuario especificado.
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
        return userContainer.GetBlobClient(name).Exists();
    }

    public BlobContainerClient CheckAndCreateContainer(string username)
    {
        // Verifica y crea el contenedor de blobs correspondiente al usuario.
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
        // Elimina un archivo de la nube para el usuario especificado.
        // Verifica si el archivo existe en la nube.
        if (!CheckCloudFile(name, username))
        {
            return false;
        }
        
        try
        {
            // Obtiene el contenedor del usuario y elimina el blob.
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
        // Elimina un archivo local del directorio especificado.
        // Verifica si el archivo existe localmente.
        if (!CheckLocalFile(name))
        {
            return false;
        }
    
        try
        {
            // Elimina el archivo del directorio.
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
        // Carga un archivo al almacenamiento en la nube.
        // Verifica si el archivo local existe.
        if (!CheckLocalFile(name))
        {
            return false;
        }
    
        // Verifica y crea el contenedor correspondiente al usuario.
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
    
        try
        {
            // Sube el archivo al contenedor en la nube.
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
        // Descarga un archivo desde el almacenamiento en la nube.
        // Verifica si el archivo existe en la nube.
        if (!CheckCloudFile(name, username))
        {
            return false;
        }
    
        // Verifica y crea el contenedor correspondiente al usuario.
        BlobContainerClient userContainer = CheckAndCreateContainer(username);
    
        try
        {
            // Elimina el archivo local si existe.
            DeleteFileFromLocal(name);
        
            // Descarga el archivo desde el contenedor en la nube y lo guarda localmente.
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