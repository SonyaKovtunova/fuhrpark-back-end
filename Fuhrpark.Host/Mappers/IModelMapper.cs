using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface IModelMapper
    { }

    public interface IModelMapper<S, T> : IModelMapper
       where S : class
       where T : class
    {
        TDestination Map<TSource, TDestination>(TSource source, Action<TSource, TDestination> extra = null, TDestination destination = null)
            where TSource : class
            where TDestination : class;

        IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source, Action<TSource, TDestination> extra = null);

        IEnumerable<S> MapCollectionFromModel(IEnumerable<T> source, Action<T, S> extra = null);
        IEnumerable<T> MapCollectionToModel(IEnumerable<S> source, Action<S, T> extra = null);
        IEnumerable<T> MapCollectionToModel(IEnumerable source, Action<S, T> extra = null);
        S MapFromModel(T source, Action<T, S> extra = null, S destination = null);
        T MapToModel(S source, Action<S, T> extra = null, T destination = null);
    }
}
