using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TalentTrack.Infrastructure.Data.Config;

public static class ModelBuilderExtensions
{
    public static void AddGlobalQueryFilter<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(TInterface).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var filter = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameter, expression.Body);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(filter, parameter));
            }
        }
    }
}
