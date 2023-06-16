using System.Windows.Input;
using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using Root_Way.Models;
using Root_Way.Repositories;

namespace Root_Way.ViewModels;

public class ForgotPasswordWindowViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private readonly IUserRepository _userRepository;
    private Window _forgotPasswordWindow;

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

    public ICommand ForgotPasswordCommand { get; }

    public ForgotPasswordWindowViewModel(Window w)
    {
        _forgotPasswordWindow = w;
        _userRepository = new UserRepository();

        ForgotPasswordCommand = new ViewModelCommand(ExecuteForgotPasswordWindowCommand);
    }

    private void ExecuteForgotPasswordWindowCommand(object obj)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            var emptyError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Information",
                    //ContentHeader = header,
                    ContentMessage = "ERROR! Username or password is empty!\n",
                    CanResize = false,
                    MaxWidth = 500,
                    MaxHeight = 100,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ShowInCenter = true,
                    Topmost = false,
                    Icon = Icon.Error
                    
                });
            emptyError.Show();
            return;
        }

        if (_userRepository.GetByUsername(Username) == null)
        {
            var errorUserAlredyExists = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Information",
                    //ContentHeader = header,
                    ContentMessage = "ERROR! Username not exists!\n",
                    CanResize = false,
                    MaxWidth = 500,
                    MaxHeight = 100,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ShowInCenter = true,
                    Topmost = false,
                    Icon = Icon.Database
                    
                });
            errorUserAlredyExists.Show();
            return;
        }
        
        //password recovery
        UserModel userModel = new UserModel
        {
            Username = Username,
            Password = Password
        };

        _userRepository.Edit(userModel);

        // Mostrar un mensaje de registro exitoso
        var successfulMessage = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                ContentTitle = "Information",
                //ContentHeader = header,
                ContentMessage = "Password successful changed!\n",
                CanResize = false,
                MaxWidth = 500,
                MaxHeight = 100,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInCenter = true,
                Topmost = false,
                Icon = Icon.Success
                    
            });
        successfulMessage.Show();
        
        
        // Cerrar la ventana de registro
        _forgotPasswordWindow.Close();
    }
}