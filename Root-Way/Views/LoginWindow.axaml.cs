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
        DataContext = new LoginWindowViewModel(this);
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}