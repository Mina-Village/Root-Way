using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Root_Way.Views.AddPages
{
    public partial class AddSubjectWindow : Window
    {
        public AddSubjectWindow()
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