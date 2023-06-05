using System.Collections.Generic;
using Root_Way.Models.Elements;

namespace Root_Way.Interfaces;

public interface IGradesContainer
{
    IGradesContainer? ParentContainer { get; }
    List<Grade> Grades { get; }
}