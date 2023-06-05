using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Root_Way.Enums;
using Root_Way.ViewModels;
using Root_Way.Views;

namespace Root_Way;

public class App : Application
{
    private const string ResourcesPath = "avares://Root-Way/Resources/";

    private ThemeMode _theme = ThemeMode.None;
    private StyleInclude? _currentThemeStyle;

    private readonly Dictionary<ThemeMode, Uri> _themeSourcesCollection = new()
    {
        { ThemeMode.Light, new Uri(Path.Combine(ResourcesPath, "LightTheme.axaml")) },
        { ThemeMode.Dark, new Uri(Path.Combine(ResourcesPath, "DarkTheme.axaml")) }
    };

    public App() => SetTheme(ThemeMode.Light); // TODO: Get from saved settings
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            /*var loginWindow = new LoginWindow();
            loginWindow.Show();
            loginWindow.Closed += (sender, e) =>
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowViewModel()
                };
                //loginWindow.Close();
            };*/
            
            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel() //{AppInstance = this}
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
    
    internal void SetTheme(ThemeMode theme)
    {
        if (theme == _theme)
            return;
        _theme = theme;

        if (_currentThemeStyle is not null)
            this.Styles.Remove(_currentThemeStyle);
        _currentThemeStyle = new StyleInclude(_themeSourcesCollection[theme])
        {
            Source = _themeSourcesCollection[theme]
        };
        this.Styles.Add(_currentThemeStyle);
    }
}