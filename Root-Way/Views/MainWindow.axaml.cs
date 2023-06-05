using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;


namespace Root_Way.Views;

public class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new HomeWindowViewModel();
        DataContext = new OsintWindowViewModel();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}