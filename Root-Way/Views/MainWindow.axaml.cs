using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Root_Way.ViewModels;


namespace Root_Way.Views;

public partial class MainWindow : Window
{
    public MainWindow(string username)
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this, username);

    }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this, "username");

    }
}