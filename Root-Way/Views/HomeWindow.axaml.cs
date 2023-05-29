using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class HomeWindow : UserControl
{
    public HomeWindow()
    {
        InitializeComponent();
        DataContext = new HomeWindowViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}