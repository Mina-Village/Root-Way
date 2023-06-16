using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
        DataContext = new RegisterWindowViewModel();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}