using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Root_Way.Views.TargetGrade
{
    public partial class TargetGradeView : UserControl
    {
        public TargetGradeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}