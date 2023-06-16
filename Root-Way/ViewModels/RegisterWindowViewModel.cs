using System.Windows.Input;
using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using Root_Way.Models;
using Root_Way.Repositories;

namespace Root_Way.ViewModels;

public class RegisterWindowViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private readonly IUserRepository _userRepository;
    private Window _registerWindow;

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

    public ICommand RegisterCommand { get; }

    public RegisterWindowViewModel(Window w)
    {
        _registerWindow = w;
        _userRepository = new UserRepository();

        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand);
    }

    private void ExecuteRegisterCommand(object obj)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            var emptyError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Register Information",
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

        if (_userRepository.GetByUsername(Username) != null)
        {
            var errorUserAlredyExists = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Register Information",
                    //ContentHeader = header,
                    ContentMessage = "ERROR! Username alredy exists!\n",
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
        
        // Aquí puedes implementar la lógica para registrar al usuario en tu repositorio
        UserModel userModel = new UserModel
        {
            Username = Username,
            Password = Password
        };

        _userRepository.Add(userModel);

        // Mostrar un mensaje de registro exitoso
        var successfulMessage = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
            new MessageBoxStandardParams
            {
                ContentTitle = "Register Information",
                //ContentHeader = header,
                ContentMessage = "Registration successful!\n",
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
        _registerWindow.Close();
    }
}
