using System;

namespace TwoDimensionalFields.Searching
{
    public interface ISearcher<T>
    {
        T Search(ISearchable<T> searchable);
    }
}
