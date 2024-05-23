using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Infrastructure
{
    public static class Extensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
        this IQueryable<T> query,
        string orderByMember,
        string direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                !string.IsNullOrEmpty(direction) && direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        public static IEnumerable<T> OrderByPropertyName<T>(this IEnumerable<T> source, string propertyName, bool descending)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"No property '{propertyName}' on type '{typeof(T).Name}'");
            }

            return descending
                ? source.OrderByDescending(x => propertyInfo.GetValue(x, null))
                : source.OrderBy(x => propertyInfo.GetValue(x, null));
        }
    }
}
