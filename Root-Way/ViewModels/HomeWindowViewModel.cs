using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace Root_Way.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private string _newTask;
    private string _selectedItem;
    private ObservableCollection<string> _tasks;
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }

    public HomeWindowViewModel()
    {
        Tasks = new ObservableCollection<string>();

        DeleteTaskCommand = new ViewModelCommand(DeleteTask);
        AddTaskCommand = new ViewModelCommand(AddTask, CanAddTask);
    }

    public string NewTask
    {
        get { return _newTask; }

        set
        {
            _newTask = value;
            OnPropertyChanged(nameof(_newTask));
        }
    }
    
    public string SelectedItem
    {
        get { return _selectedItem; }

        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(_selectedItem));
        }
    }
    
    public ObservableCollection<string> Tasks
    {
        get { return _tasks; }

        set
        {
            _tasks = value;
            OnPropertyChanged(nameof(_tasks));
        }
    }

    private void AddTask(object obj)
    {
        if (!string.IsNullOrEmpty(NewTask))
        {
            Tasks.Add(NewTask);
            NewTask = string.Empty;
        }
    }
    
    private bool CanAddTask(object obj)
    {
        return true;
        // return !string.IsNullOrEmpty(NewTask);
    }

    private void DeleteTask(object obj)
    {
        Tasks.Remove(SelectedItem);
    }

    private bool CanDeleteTask(object obj)
    {
        return !string.IsNullOrEmpty(SelectedItem);
    }
}
