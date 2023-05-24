using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using Root_Way.Models;
using Root_Way.Repositories;
using Root_Way.Views;

namespace Root_Way.ViewModels;

public class LoginWindowViewModel : ViewModelBase
{
    public ICommand LoginCommand { get; }

    public LoginWindowViewModel()
    {
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
    }
    

    private bool CanExecuteLoginCommand(object obj)
    {
        return true;
    }
    
    private void ExecuteLoginCommand(object obj)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        
    }
    
}