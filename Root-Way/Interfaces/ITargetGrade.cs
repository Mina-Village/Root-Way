using System.Collections.Generic;
using Root_Way.Models.Elements;

namespace Root_Way.Interfaces
{
    public interface ITargetGrade
    {
        IEnumerable<Grade>? Grades { get; set; }
    }
}