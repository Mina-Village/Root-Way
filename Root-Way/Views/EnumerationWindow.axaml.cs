using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ReactiveUI;
using Root_Way.ViewModels;

namespace Root_Way.Views;

public partial class EnumerationWindow : UserControl
{
    public EnumerationWindow()
    {
        InitializeComponent();
        DataContext = new EnumerationWindowViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}