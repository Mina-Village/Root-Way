using System.ComponentModel;
using Avalonia.Controls;
using ReactiveUI;
using Root_Way.Models;
using Root_Way.Models.Settings;
using Root_Way.Views;

namespace Root_Way.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected HomeWindow? HomeWindowInstance { get; private set; }
    protected Preferences? SettingsRef { get; }
        
    protected Controls? TopbarTexts { get; private set; }
    protected internal Window? CurrentAddWindow { get; internal set; }

    protected ViewModelBase() 
        => this.SettingsRef = SettingsManager.Settings;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected void InitializeTopbarElements()
    {
        if (HomeWindow.Instance is null)
            return;
            
        this.HomeWindowInstance = HomeWindow.Instance;
        this.TopbarTexts = this.HomeWindowInstance.Get<Grid>("Topbar-Grid").Children;
    }
        
    protected void UpdateVisualOnChange()
    {
        CloseAddWindow();
        DataManager.SaveData();
    }

    protected internal virtual void EraseData() { }

    private void CloseAddWindow()
    {
        CurrentAddWindow?.Close();
        CurrentAddWindow = null;
    }
}