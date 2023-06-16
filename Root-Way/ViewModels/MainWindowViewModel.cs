using System.Windows.Input;
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
    //private IconChar _icon;
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
    public MainWindowViewModel()
    {
        //userRepository = new UserRepository();
        //CurrentUserAccount = new UserAccountModel();
        //Initialize commands
        ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        ShowEnumerationViewCommand = new ViewModelCommand(ExecuteShowEnumertaionViewCommand);
        ShowOsintViewCommand = new ViewModelCommand(ExecuteShowOsintViewCommand);
        ShowExploitationViewCommand = new ViewModelCommand(ExecuteShowExploitationViewCommand);
        //Default view
        //ExecuteShowHomeViewCommand(null);
        //LoadCurrentUserData();
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
    
}