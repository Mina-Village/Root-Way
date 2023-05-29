using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels;
using Root_Way.Views;

namespace Root_Way;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(),
            };
            
            /*var loginWindow = new LoginWindow();
            loginWindow.Show();
            loginWindow.Closed += (sender, e) =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                loginWindow.Close();
            };*/
        }

        base.OnFrameworkInitializationCompleted();
    }
}