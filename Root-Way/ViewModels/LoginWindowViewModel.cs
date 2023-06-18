using System.Net;
using System.Windows.Input;
using Avalonia.Controls;
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
    private Window _loginWindow;

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

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand ForgotPasswordCommand { get; }

    public LoginWindowViewModel(Window w)
    {
        _loginWindow = w;
        userRepository = new UserRepository();
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand);
        //ForgotPasswordCommand = new ViewModelCommand(ExecuteForgotPasswordCommand);
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
            var mainWindow = new MainWindow(Username);
            mainWindow.Show();
            _loginWindow.Close();
        }
        else
        {
            ErrorMessage = "* Invalid username or password";
        }
    }
    
    private void ExecuteRegisterCommand(object obj)
    {
        var registerWindow = new RegisterWindow();
        registerWindow.Show();
    }
}