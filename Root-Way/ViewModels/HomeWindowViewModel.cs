using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;

namespace Root_Way.ViewModels;

public class HomeWindowViewModel : ViewModelBase, IReactiveObject
{
    private string newTask;
    private ObservableCollection<string> tasks;

    public HomeWindowViewModel()
    {
        tasks = new ObservableCollection<string>();

        AddTaskCommand = ReactiveCommand.Create(AddTask);
        DeleteTaskCommand = ReactiveCommand.Create<string>(DeleteTask);
    }

    public string NewTask
    {
        get { return newTask; }
        set { this.RaiseAndSetIfChanged(ref newTask, value); }
    }

    public ObservableCollection<string> Tasks
    {
        get { return tasks; }
        set { this.RaiseAndSetIfChanged(ref tasks, value); }
    }

    public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }
    public ReactiveCommand<string, Unit> DeleteTaskCommand { get; }

    private void AddTask()
    {
        if (!string.IsNullOrEmpty(NewTask))
        {
            Tasks.Add(NewTask);
            NewTask = string.Empty;
        }
    }

    private void DeleteTask(string task)
    {
        Tasks.Remove(task);
    }

    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new System.NotImplementedException();
    }
}
