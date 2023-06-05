using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Root_Way.Views.TargetGrade
{
    public partial class AverageGradeView : UserControl
    {
        public AverageGradeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}