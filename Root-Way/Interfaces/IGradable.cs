namespace Root_Way.Interfaces
{
    public interface IGradable
    {
        float GradeValue { get; }
        float Weighting { get; }
        bool Counts { get; }
        int ElementCount { get; }
    }
}