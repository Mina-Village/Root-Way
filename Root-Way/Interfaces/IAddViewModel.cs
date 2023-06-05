namespace Root_Way.Interfaces
{
    public interface IAddViewModel<in T> where T : IElement
    {
        void EditElement(T element);
    }
}