using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Projektanker.Icons.Avalonia;
using ReactiveUI;
using Root_Way.Models;
using Root_Way.Repositories;

namespace Root_Way.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    //Fields
    //private UserAccountModel _currentUserAccount;
    private ViewModelBase _currentChildView;
    private string _caption;
    private Icon _icon;
    private IUserRepository userRepository;

    //Properties
    /*public UserAccountModel CurrentUserAccount
    {
        get
        {
            return _currentUserAccount;
        }
        set
        {
            _currentUserAccount = value;
            OnPropertyChanged(nameof(CurrentUserAccount));
        }
    }*/
    public ViewModelBase CurrentChildView
    {
        get => _currentChildView;
        set
        {
            _currentChildView = value;
            OnPropertyChanged(nameof(CurrentChildView));
        }
    }
    public string Caption
    {
        get => _caption;
        set
        {
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }
    public Icon Icon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
            OnPropertyChanged(nameof(Icon));
        }
    }
    //--> Commands
    public ICommand ShowHomeViewCommand { get; }
    public ICommand ShowCustomerViewCommand { get; }
    public MainWindowViewModel()
    {
        //userRepository = new UserRepository();
        //CurrentUserAccount = new UserAccountModel();
        //Initialize commands
        ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
        //Default view
        //ExecuteShowHomeViewCommand(null);
        //LoadCurrentUserData();
    }

    private void ExecuteShowCustomerViewCommand(object obj)
    {
        CurrentChildView = new OsintWindowViewModel();
        Caption = "OSINT";
        //ICON

    }
    private void ExecuteShowHomeViewCommand(object obj)
    {
        CurrentChildView = new HomeWindowViewModel();
        Caption = "Home";
        //Icon = Icon.Home;
    }
    
}