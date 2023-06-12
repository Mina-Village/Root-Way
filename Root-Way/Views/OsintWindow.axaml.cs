using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.Models;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class OsintWindow : UserControl
{
    public OsintWindow()
    {
        InitializeComponent();
        DataContext = new OsintWindowViewModel();
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}