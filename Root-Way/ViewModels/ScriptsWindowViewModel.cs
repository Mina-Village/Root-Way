using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Threading;
using DynamicData;
using Root_Way.Models;
using Root_Way.Repositories;

namespace Root_Way.ViewModels;

public class ScriptsWindowViewModel : ViewModelBase
{
    private FileModel _selectedItem;
    private ObservableCollection<FileModel> _scripts = new ObservableCollection<FileModel>();
    private string _username;
    private StorageRepository storageRepository;
    
    public ICommand RefreshCommand { get; }
    public ICommand DownloadCommand { get; }
    public ICommand UploadCommand { get; }
    public ICommand DeleteFromCloudCommand { get; }
    public ICommand DeleteFromLocalCommand { get; }

    public ScriptsWindowViewModel()
    {
        
    }

    public ScriptsWindowViewModel(string username)
    {
        storageRepository = new StorageRepository();
        Username = username;
        ExecuteRefreshCommand(null);
        RefreshCommand = new ViewModelCommand(ExecuteRefreshCommand);
        DownloadCommand = new ViewModelCommand(ExecuteDownloadCommand);
        UploadCommand = new ViewModelCommand(ExecuteUploadCommand);
        DeleteFromCloudCommand = new ViewModelCommand(ExecuteDeleteFromCloudCommand);
        DeleteFromLocalCommand = new ViewModelCommand(ExecuteDeleteFromLocalCommand);
    }
    public FileModel SelectedItem
    {
        get { return _selectedItem; }

        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(_selectedItem));
        }
    }
    public string Username
    {
        get { return _username; }

        set
        {
            _username = value;
            OnPropertyChanged(nameof(_username));
        }
    }
    
    public ObservableCollection<FileModel> Scripts
    {
        get { return _scripts; }

        set
        {
            _scripts = value;
            OnPropertyChanged(nameof(_scripts));
        }
        
    }
    public void ExecuteRefreshCommand (object obj)
    {
        ObservableCollection<FileModel> newScripts = storageRepository.GetAllScripts(Username);
        foreach (FileModel doc in Scripts.ToList())
        {
            Scripts.Remove(doc);
        }
        foreach (FileModel doc in newScripts)
        {
            Scripts.Add(doc);
        }
        foreach (FileModel doc in Scripts)
        {
            Console.WriteLine(doc.Name + " " + doc.FileType);
        }
    }
    public void ExecuteDownloadCommand (object obj)
    {
        storageRepository.DownloadFile(SelectedItem.Name, Username);
        ExecuteRefreshCommand(null);
    }
    public void ExecuteUploadCommand (object obj)
    {
        storageRepository.UploadFile(SelectedItem.Name, Username);
        ExecuteRefreshCommand(null);
    }
     public void ExecuteDeleteFromCloudCommand (object obj)
    {
        storageRepository.DeleteFileFromCloud(SelectedItem.Name, Username);
        ExecuteRefreshCommand(null);
    }
     public void ExecuteDeleteFromLocalCommand (object obj)
     {
         storageRepository.DeleteFileFromLocal(SelectedItem.Name);
         ExecuteRefreshCommand(null);
     }
    public bool SelectedScriptIsCloud (object obj)
    {
        return SelectedItem.FileType == FileType.Cloud || SelectedItem.FileType == FileType.Both;
    }

    public bool SelectedScriptIsLocal(object obj)
    {
        return SelectedItem.FileType == FileType.Local || SelectedItem.FileType == FileType.Both;
    }
}