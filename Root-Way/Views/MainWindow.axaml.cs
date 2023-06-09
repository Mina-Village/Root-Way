using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Root_Way.ViewModels;


namespace Root_Way.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new HomeWindowViewModel();
        DataContext = new EnumerationWindowViewModel();
        DataContext = new OsintWindowViewModel();
       
    }
}