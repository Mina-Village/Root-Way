using System;
using System.Windows.Input;
using Avalonia.Controls;
using Root_Way.Models;
using Root_Way.Repositories;

namespace Root_Way.ViewModels;

public class RegisterWindowViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private readonly IUserRepository _userRepository;

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

    public RegisterWindowViewModel()
    {
        _userRepository = new UserRepository();

        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand);
    }

    private void ExecuteRegisterCommand(object obj)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            // TODO error cosas
            Console.WriteLine("Contraseña o username incorrecto");
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
        //MessageBox.Show("Registration successful!");

        // Cerrar la ventana de registro
        Window window = obj as Window;
        window.Close();
    }
}
