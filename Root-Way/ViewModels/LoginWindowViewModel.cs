using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Security;
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
    //Fields
    private string _username;
    private string _password;
    private string _errorMessage;
    private bool _isViewVisible = true;

    private IUserRepository userRepository;

    //Properties
    public string Username
    {
        get { return _username; }

        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get { return _password; }

        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string ErrorMessage
    {
        get { return _errorMessage; }

        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public bool IsViewVisible
    {
        get { return _isViewVisible; }

        set
        {
            _isViewVisible = value;
            OnPropertyChanged(nameof(IsViewVisible));
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public LoginWindowViewModel()
    {
        userRepository = new UserRepository();
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand);
    }

    private bool CanExecuteLoginCommand(object obj)
    {
        return true;
    }

    private void ExecuteLoginCommand(object obj)
    {
        var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
        if (isValidUser)
        {
            var mainWindow = new MainWindow();
            var loginWindow = new LoginWindow();
            mainWindow.Show();
            //aqui hay que cambiar algo, porque con el codigo de abajo no arranca el mainwindow
            //si lo ponga a mano si que va, pero hace algo raro, como si lo abriera 2 veces pero estuviese oculto.
            //pero así funciona
            /*Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity(Username), null);
            IsViewVisible = false;*/
        }
        else
        {
            ErrorMessage = "* Invalid username or password";
        }
    }
    
    private void ExecuteRegisterCommand(object obj)
    {
        var registerWindow = new RegisterWindow()
        {
            DataContext = new RegisterWindowViewModel()
        };
        registerWindow.Show();
    }
}