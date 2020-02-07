using System;

namespace TwoDimensionalFields.Searching
{
    public interface ISearchable<T>
    {
        T Search(ISearcher<T> searcher);
    }
}
