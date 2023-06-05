using Root_Way.Enums;

namespace Root_Way.Models.Settings
{
    public class Preferences
    {
        public SelectedButtonStyle YearButtonStyle { get; set; } = SelectedButtonStyle.Grid;
        public SelectedButtonStyle SubjectButtonStyle { get; set; } = SelectedButtonStyle.Grid;
        public SelectedButtonStyle GradeButtonStyle { get; set; } = SelectedButtonStyle.Grid;

        public bool ShowRemoveConfirmation { get; set; } = true;
    }
}