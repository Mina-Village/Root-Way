using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Root_Way.Models;
using Root_Way.Repositories;
using Root_Way.ViewModels;
using Root_Way.Views;

namespace Root_Way;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new LoginWindow();

            /*var loginWindow = new LoginWindow();
            loginWindow.Show();
            loginWindow.Closed += (sender, e) =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                loginWindow.Close();
                desktop.MainWindow = new LoginWindow()
                {
                    DataContext = new LoginWindowViewModel()
                };
                
            };*/
        }

        base.OnFrameworkInitializationCompleted();
    }
}