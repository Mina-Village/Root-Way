using Avalonia.Markup.Xaml;
using Root_Way.Models;

namespace Root_Way.Views.Lists
{
    public class SubjectListView : DragControl
    {
        public SubjectListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}