using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using TestTemplate4.Application.Sorting.Models;
using TestTemplate4.Common.Base;

namespace TestTemplate4.Data.QueryableExtensions
{
    public static class Sorting
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IEnumerable<SortCriteria> sortCriteria)
            where T : BaseEntity<Guid>
         => sortCriteria.Any()
            ? query.OrderBy(string.Join(',', sortCriteria))
            : query.OrderBy(x => x.Id);
    }
}
