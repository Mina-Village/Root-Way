using Avalonia.Media;
using ReactiveUI;

namespace Root_Way.Models
{
    public class ColorRepresentation : ReactiveObject
    {
        private bool _selected;

        public ColorRepresentation(Color color)
        {
            ElementColor = color;
            ElementColorBrush = new SolidColorBrush(color);
        }
        
        internal Color ElementColor { get; }
        internal SolidColorBrush ElementColorBrush { get; }
        internal bool Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }
}