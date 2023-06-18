using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class ScriptsWindow : UserControl
{
    public ScriptsWindow()
    {
        InitializeComponent();
    }
    public ScriptsWindow(string username)
    {
        InitializeComponent();
        DataContext = new ScriptsWindowViewModel(username);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}