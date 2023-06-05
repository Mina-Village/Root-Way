using Avalonia.Markup.Xaml;
using Root_Way.Models;

namespace Root_Way.Views.Lists
{
    public class YearListView : DragControl
    {
        public YearListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}