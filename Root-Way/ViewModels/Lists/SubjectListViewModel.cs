﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Root_Way.Enums;
using Root_Way.Interfaces;
using Root_Way.Models.Elements;
using Root_Way.Models.Settings;
using Root_Way.ViewModels.AddPages;
using Root_Way.ViewModels.BaseClasses;
using Root_Way.Views.AddPages;
using ReactiveUI;
using Root_Way.ViewModels;

namespace Root_Way.ViewModels.Lists
{
    public class SubjectListViewModel : ListViewModelBase, IListViewModel<Subject>
    {
        private readonly bool[] _elementsVisibilities = { true, false, false, false, false, false, false };
        private ObservableCollection<Subject>? _items;

        public SubjectListViewModel(IEnumerable<Subject> items)
        {
            Instance = this;
            AddPageType = typeof(AddSubjectWindow);
            AddViewModelType = typeof(AddSubjectViewModel);
            ElementType = typeof(Subject);
            
            bool isGrid = SettingsManager.Settings?.SubjectButtonStyle == SelectedButtonStyle.Grid;
            ChangeButtonView(isGrid);
            
            var homeInstance = HomeWindowViewModel.Instance;
            Items = new ObservableCollection<Subject>(items);
            Items.CollectionChanged += (sender, args) =>
            {
                this.RaisePropertyChanged(nameof(EmptyCollection));
                homeInstance?.UpdateAverage();
            };
            InitializeTopbarElements();
        }
        
        public ObservableCollection<Subject>? Items
        {
            get => _items;
            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
                this.RaisePropertyChanged(nameof(EmptyCollection));
            }
        }
        
        public bool EmptyCollection => Items?.Count == 0;
        
        internal static SubjectListViewModel? Instance { get; private set; }

        public void Duplicate(IElement element) 
            => DuplicateElement<Subject>(element);

        /*protected internal override void ChangeTopbar()
        {
            base.ChangeTopbar();
            for (int i = 0; i < TopbarTexts!.Count; i++)
                TopbarTexts[i].IsVisible = _elementsVisibilities[i];
        }*/

        private void RemoveElement(Subject subject)
        {
            var currentYear = HomeWindowViewModel.CurrentYear;
            Action action = () =>
            {
                currentYear?.Subjects.Remove(subject);
                Items?.Remove(subject);
                UpdateVisualOnChange();
            };
            base.RemoveElement(subject, Enums.ElementType.Subject, action);
        }
    }
}