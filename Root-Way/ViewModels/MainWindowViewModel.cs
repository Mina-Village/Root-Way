using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;
using Root_Way.Models;
using Root_Way.Repositories;
using Root_Way.Views;

namespace Root_Way.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    //Fields
    private string _currentUserAccount;
    private ViewModelBase _currentChildView;
    private ViewModelBase _closeUserSession;
    private string _caption;
    //private IconChar _icon;
    private IUserRepository userRepository;
    private Window _mainWindow;

    //Properties
    public string CurrentUserAccount
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
    }
    
    public ViewModelBase CloseUserSession
    {
        get
        {
            return _closeUserSession;
        }
        set
        {
            _closeUserSession = value;
            OnPropertyChanged(nameof(CloseSessionCommand));
        }
    }
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
    
    /*public IconChar Icon
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
    }*/
    //--> Commands
    public ICommand ShowHomeViewCommand { get; }
    public ICommand ShowOsintViewCommand { get; }
    public ICommand ShowEnumerationViewCommand { get; }
    public ICommand ShowExploitationViewCommand { get; }
    public ICommand ShowScriptsViewCommand { get; }
    public ICommand ForgotPasswordCommand { get; }
    public ICommand CloseSessionCommand { get; }

    public MainWindowViewModel(Window w, string Username)
    {
        userRepository = new UserRepository();
        CurrentUserAccount = Username;
        _mainWindow = w;

        //Initialize commands
        ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        ShowEnumerationViewCommand = new ViewModelCommand(ExecuteShowEnumertaionViewCommand);
        ShowOsintViewCommand = new ViewModelCommand(ExecuteShowOsintViewCommand);
        ShowExploitationViewCommand = new ViewModelCommand(ExecuteShowExploitationViewCommand);
        ShowScriptsViewCommand = new ViewModelCommand(ExecuteShowScriptsViewCommand);
        ForgotPasswordCommand = new ViewModelCommand(ExecuteForgotPasswordCommand);
        CloseSessionCommand = new ViewModelCommand(ExecuteCloseSessionCommand);
        
        //Default view
        //ExecuteShowHomeViewCommand(null);
        //LoadCurrentUserData();
    }
    
    private void ExecuteForgotPasswordCommand(object obj)
    {
        var forgotPasswordWindow = new ForgotPasswordWindow(CurrentUserAccount);
        
        forgotPasswordWindow.Show();
    }

    private void ExecuteShowEnumertaionViewCommand(object obj)
    {
        CurrentChildView = new EnumerationWindowViewModel();
        Caption = "Enumeration";
        //Icon = IconChar.UserGroup;
    }
    
    private void ExecuteShowOsintViewCommand(object obj)
    {
        CurrentChildView = new OsintWindowViewModel();
        Caption = "OSINT";
        //Icon = IconChar.UserGroup;
    }
    private void ExecuteShowHomeViewCommand(object obj)
    {
        CurrentChildView = new HomeWindowViewModel();
        Caption = "Home";
        //Icon = IconChar.Home;
    }
    
    private void ExecuteShowExploitationViewCommand(object obj)
    {
        CurrentChildView = new ExploitationWindowViewModel();
        Caption = "Exploitation";
        //Icon = IconChar.Home;
    }
    private void ExecuteShowScriptsViewCommand(object obj)
    {
        CurrentChildView = new ScriptsWindowViewModel(CurrentUserAccount);
        Caption = "Scripts";
        //Icon = IconChar.Home;
    }
    private void ExecuteCloseSessionCommand(object obj)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        _mainWindow.Close();
        //Icon = IconChar.Home;
    }
    
}