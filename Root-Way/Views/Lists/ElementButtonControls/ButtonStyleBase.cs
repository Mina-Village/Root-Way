using Root_Way.Interfaces;
using Root_Way.Models.Elements;

namespace Root_Way.Views.Lists.ElementButtonControls
{
    public abstract class ButtonStyleBase
    {
        protected ButtonStyleBase(IElement element)
        {
            switch (element)
            {
                case SchoolYear year:
                    YearRef = year;
                    break;
                case Subject subject:
                    SubjectRef = subject;
                    break;
                case Grade grade:
                    GradeRef = grade;
                    break;
            }
        }
        
        protected SchoolYear? YearRef { get; }
        protected Subject? SubjectRef { get; }
        protected Grade? GradeRef { get; }
    }
}