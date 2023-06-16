using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
       InitializeComponent();
       DataContext = new LoginWindowViewModel();
       DataContext = new RegisterWindowViewModel();
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
    }
}