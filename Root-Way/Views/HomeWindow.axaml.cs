using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Root_Way.Interfaces;
using Root_Way.Models;
using Root_Way.Models.Elements;
using Root_Way.ViewModels;
using Root_Way.ViewModels.BaseClasses;
using Root_Way.ViewModels.Lists;

namespace Root_Way.Views;

public class HomeWindow : Window
{
    private HomeWindowViewModel? _homeWindowModel;

    public HomeWindow()
    {
        InitializeComponent();
        DataContext = new HomeWindowViewModel();
#if DEBUG
        this.AttachDevTools();
#endif
        Instance = this;
    }

    internal static HomeWindow? Instance { get; private set; }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnYearPressed(object? sender, PointerPressedEventArgs e)
    {
        SwitchPage<YearListViewModel, SchoolYear>(DataManager.SchoolYears);
        HomeWindowViewModel.CurrentYear = null;
    }
        
    private void OnSubjectPressed(object? sender, PointerPressedEventArgs e)
    {
        SwitchPage<SubjectListViewModel, Subject>(HomeWindowViewModel.CurrentYear!.Subjects);
    }

    private void OnGradePressed(object? sender, PointerPressedEventArgs e)
    {
        // ReSharper disable once MergeIntoPattern
        if (HomeWindowViewModel.Instance?.Content is GradeListViewModel viewModel && viewModel.GradesContainer?.ParentContainer != null)
            SwitchPage<GradeListViewModel, Grade>(viewModel.GradesContainer.ParentContainer.Grades, viewModel.GradesContainer.ParentContainer);
    }

    private void SwitchPage<TViewModel, TElement>(IEnumerable<TElement> elements, IGradesContainer? gradesContainer = null)
        where TViewModel : ListViewModelBase, IListViewModel<TElement> 
        where TElement : class, IElement, IGradable
    {
        _homeWindowModel ??= this.DataContext as HomeWindowViewModel;
        _homeWindowModel!.SwitchPage<TViewModel, TElement>(elements, gradesContainer);
    }
}