using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Root_Way.Views.TargetGrade
{
    public partial class TargetGradeWindow : Window
    {
        public TargetGradeWindow()
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
    }
}