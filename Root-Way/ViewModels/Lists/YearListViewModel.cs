using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Root_Way.Enums;
using Root_Way.Interfaces;
using Root_Way.Models;
using Root_Way.Models.Elements;
using Root_Way.Models.Settings;
using Root_Way.ViewModels.AddPages;
using Root_Way.ViewModels.BaseClasses;
using Root_Way.Views.AddPages;
using ReactiveUI;
using Root_Way.ViewModels;

namespace Root_Way.ViewModels.Lists
{
    public class YearListViewModel : ListViewModelBase, IListViewModel<SchoolYear>
    {
        private ObservableCollection<SchoolYear>? _items;
        
        public YearListViewModel(IEnumerable<SchoolYear> years)
        {
            Instance = this;
            AddPageType = typeof(AddYearWindow);
            AddViewModelType = typeof(AddYearViewModel);
            ElementType = typeof(SchoolYear);
            
            bool isGrid = SettingsManager.Settings?.YearButtonStyle == SelectedButtonStyle.Grid;
            ChangeButtonView(isGrid);
            
            var homeInstance = HomeWindowViewModel.Instance;
            Items = new ObservableCollection<SchoolYear>(years);
            Items.CollectionChanged += (sender, args) =>
            {
                this.RaisePropertyChanged(nameof(EmptyCollection));
                homeInstance?.UpdateAverage();
            };
            InitializeTopbarElements();
        }

        public ObservableCollection<SchoolYear>? Items
        {
            get => _items;
            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
                this.RaisePropertyChanged(nameof(EmptyCollection));
            }
        }
        
        public bool EmptyCollection => Items?.Count == 0;
        
        internal static YearListViewModel? Instance { get; private set; }

        public void Duplicate(IElement element) 
            => DuplicateElement<SchoolYear>(element);

        /*protected internal override void ChangeTopbar()
        {
            base.ChangeTopbar();
            foreach (var control in TopbarTexts!)
                control.IsVisible = false;
        }*/

        private void RemoveElement(SchoolYear year)
        {
            Action action = () =>
            {
                DataManager.SchoolYears.Remove(year);
                Items?.Remove(year);
                UpdateVisualOnChange();
            };
            base.RemoveElement(year, Enums.ElementType.SchoolYear, action);
        }
    }
}