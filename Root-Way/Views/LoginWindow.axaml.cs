using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public class LoginWindow : Window
{
    public LoginWindow()
    {
       InitializeComponent();
       DataContext = new LoginWindowViewModel();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
    }
}