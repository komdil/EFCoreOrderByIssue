using System.Linq.Expressions;

namespace EFOrderByIssue
{
    public static class Extensions
    {
        public static IQueryable<T> OrderByItems<T, TItem>(this IQueryable<T> query, Expression<Func<T, TItem>> prop, IEnumerable<TItem> items)
        {
            var guid = items.Take(1546).Last();
            var conditions = items.Take(1546)
                .Select(item => Expression.Equal(prop.Body, Expression.Constant(item, typeof(TItem))))
                .ToList();

            // nothing to sort
            if (conditions.Count == 0)
                return query;

            Expression orderExpr = Expression.Constant(conditions.Count);

            for (var i = conditions.Count - 1; i >= 0; i--)
            {
                var condition = conditions[i];
                orderExpr = Expression.Condition(condition, Expression.Constant(i), orderExpr);
            }

            var entityParam = prop.Parameters[0];
            var orderLambda = Expression.Lambda<Func<T, int>>(orderExpr, entityParam);

            return query.OrderBy(orderLambda);
        }
    }
}
