using Avalonia.Markup.Xaml;
using Root_Way.Models;

namespace Root_Way.Views.Lists
{
    public class GradeListView : DragControl
    {
        public GradeListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}