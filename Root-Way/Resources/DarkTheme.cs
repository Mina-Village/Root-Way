using Avalonia;
using Avalonia.Media;
using Root_Way.UtilityCollection;

namespace Root_Way.ResourcesNamespace
{
    public class DarkTheme : Theme
    {
        public DarkTheme()
        {
            SetResources();
        }

        protected internal override void SetResources()
        {
            OppositeAccentBrush = 
                Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "OppositeAccent", StyleIndex) 
                ?? new SolidColorBrush(Color.Parse("#FFFFFF"));
            SameAccentBrush = 
                Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "SameAccent", StyleIndex) 
                ?? new SolidColorBrush(Color.Parse("#0A0A0A"));
        }
    }
}