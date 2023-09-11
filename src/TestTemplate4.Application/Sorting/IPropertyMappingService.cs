using System.Collections.Generic;
using TestTemplate4.Application.Sorting.Models;

namespace TestTemplate4.Application.Sorting
{
    public interface IPropertyMappingService
    {
        IEnumerable<SortCriteria> Resolve(BaseSortable sortableSource, BaseSortable sortableTarget);
    }
}
