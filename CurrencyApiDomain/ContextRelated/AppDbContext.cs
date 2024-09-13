using CurrencyApiInfrastructure.DbBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CurrencyApiDomain.ContextRelated
{
    /// <summary>
    /// The app db context.
    /// </summary>
    public partial class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Model.GetEntityTypes()
                           .Where(entityType => typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                           .ToList()
                           .ForEach(entityType =>
                           {
                               builder.Entity(entityType.ClrType)
                                      .HasQueryFilter(ConvertFilterExpressionForSoftDeletables(x => x.DeletedAt == null, entityType.ClrType));
                           });

            base.OnModelCreating(builder);
        }


        /// <summary>
        /// Convert filter expression for soft deletables.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="entityType">The entity type.</param>
        /// <returns>A LambdaExpression</returns>
        private static LambdaExpression ConvertFilterExpressionForSoftDeletables(
                            Expression<Func<ISoftDeletable, bool>> filterExpression,
                            Type entityType)
        {
            var newParam = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

            return Expression.Lambda(newBody, newParam);
        }
    }
}
