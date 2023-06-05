using System.Collections.ObjectModel;

namespace Root_Way.Interfaces
{
    public interface IListViewModel<T> where T : class, IElement
    {
        ObservableCollection<T>? Items { get; set; }
        bool EmptyCollection { get; }
        
        void Duplicate(IElement element);
    }
}