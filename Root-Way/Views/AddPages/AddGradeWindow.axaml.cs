using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Root_Way.ViewModels.AddPages;

namespace Root_Way.Views.AddPages
{
    public partial class AddGradeWindow : Window
    {
        public AddGradeWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DisplayDateChanged(object? sender, SelectionChangedEventArgs args)
        {
            if(DataContext is AddGradeViewModel viewModel)
                viewModel.DateChanged(sender, args);
        }
    }
}