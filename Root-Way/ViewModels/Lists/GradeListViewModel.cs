using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Root_Way.Enums;
using Root_Way.Interfaces;
using Root_Way.Models.Elements;
using Root_Way.Models.Settings;
using Root_Way.UtilityCollection;
using Root_Way.ViewModels.AddPages;
using Root_Way.ViewModels.BaseClasses;
using Root_Way.ViewModels.TargetGrade;
using Root_Way.Views.AddPages;
using Root_Way.Views.TargetGrade;
using ReactiveUI;
using Root_Way.ViewModels;

namespace Root_Way.ViewModels.Lists
{
    public class GradeListViewModel : ListViewModelBase, IListViewModel<Grade>
    {
        private ObservableCollection<Grade>? _items;
        private TargetGradeWindowModel? _targetGradeWindowModel;
        
        public GradeListViewModel(IEnumerable<Grade> items, IGradesContainer gradesContainer)
        {
            Instance = this;
            AddPageType = typeof(AddGradeWindow);
            AddViewModelType = typeof(AddGradeViewModel);
            ElementType = typeof(Grade);
            GradesContainer = gradesContainer;
            
            bool isGrid = SettingsManager.Settings?.GradeButtonStyle == SelectedButtonStyle.Grid;
            ChangeButtonView(isGrid);
            
            var homeInstance = HomeWindowViewModel.Instance;
            Items = new ObservableCollection<Grade>(items);
            Items.CollectionChanged += (sender, args) =>
            {
                this.RaisePropertyChanged(nameof(EmptyCollection));
                homeInstance?.UpdateAverage();
            };
            InitializeTopbarElements();
        }

        public ObservableCollection<Grade>? Items
        {
            get => _items;
            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
                this.RaisePropertyChanged(nameof(EmptyCollection));
            }
        }
        
        public bool EmptyCollection => Items?.Count == 0;
        
        internal IGradesContainer? GradesContainer { get; set; }
        
        internal static GradeListViewModel? Instance { get; private set; }

        public void Duplicate(IElement element)
        {
            if(element is GradeGroup)
                DuplicateElement<GradeGroup>(element);
            else
                DuplicateElement<Grade>(element);
        }

        /*protected internal override void ChangeTopbar()
        {
            base.ChangeTopbar();
            
            int[] additionalGradesIndices = {3, 4};
            const int gradeBorder = 3;
            bool parentIsGrade = GradesContainer?.ParentContainer is GradeGroup;
            bool containerIsGrade = GradesContainer is GradeGroup;
            for (int i = 0; i < TopbarTexts!.Count; i++)
            {
                IControl control = TopbarTexts[i];
                control.IsVisible = true;
                if (i >= gradeBorder)
                    control.IsVisible = containerIsGrade;
                if (additionalGradesIndices.Contains(i))
                    control.IsVisible = parentIsGrade;
            }
        }*/

        private void RemoveElement(Grade grade)
        {
            Action action = () =>
            {
                GradesContainer?.Grades.Remove(grade);
                Items?.Remove(grade);
                UpdateVisualOnChange();
            };
            base.RemoveElement(grade, Enums.ElementType.Grade, action);
        }

        private void OpenTargetGradeCalc()
        {
            if (Items is null)
                return;
            var window = new TargetGradeWindow();
            _targetGradeWindowModel ??= new TargetGradeWindowModel(Items);
            _targetGradeWindowModel.ClearData();
            _targetGradeWindowModel.ConfigureViewModels(Items);
            window.DataContext = _targetGradeWindowModel;
            
            Utilities.ShowDialog(window, HomeWindowInstance);
        }

        public GradeListViewModel()
        {
        }
    }
}