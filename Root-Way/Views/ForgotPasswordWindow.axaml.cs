using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class ForgotPasswordWindow : Window
{
    public ForgotPasswordWindow()
    {
        InitializeComponent();
        DataContext = new ForgotPasswordWindowViewModel(this);
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}