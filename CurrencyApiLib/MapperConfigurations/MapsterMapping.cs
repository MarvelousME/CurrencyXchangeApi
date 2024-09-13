using CurrencyApiInfrastructure.MapperConfigurations;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyApiLib.MapperConfigurations
{
    /// <summary>
    /// The mapster mapping.
    /// </summary>
    public class MapsterMapping : ICustomMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : class where TSource : class
        {
            return source.Adapt<TDestination>();
        }

        public TDestination MapToExistingObject<TSource, TDestination>(TSource source, TDestination destination) where TDestination : class where TSource : class
        {
            return source.Adapt(destination);
        }

        public IEnumerable<TDestination> MapAsNumarable<TSource, TDestination>(IQueryable<TSource> source) where TDestination : class where TSource : class
        {
            return source.ProjectToType<TDestination>();
        }

        public IQueryable<TDestination> MapAsQueryable<TSource, TDestination>(IQueryable<TSource> source) where TDestination : class where TSource : class
        {
            return source.ProjectToType<TDestination>();
        }
    }
}
