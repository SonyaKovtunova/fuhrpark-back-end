using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public abstract class AbstractModelMapper
    {
        protected abstract AutoMapper.IMapper Configure();

        protected AutoMapper.IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = Configure();
                }

                return _mapper;
            }
        }

        private AutoMapper.IMapper _mapper;
    }

    public abstract class AbstractModelMapper<S, T> : AbstractModelMapper, IModelMapper<S, T>
        where S : class
        where T : class
    {
        public IEnumerable<T> MapCollectionToModel(IEnumerable source, Action<S, T> extra = null)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                result.Add(MapToModel((S)item, extra));
            }

            return result;
        }

        public IEnumerable<S> MapCollectionFromModel(IEnumerable<T> model, Action<T, S> extra = null)
        {
            return model.Select(x => MapFromModel(x, extra)).ToList();
        }

        public IEnumerable<T> MapCollectionToModel(IEnumerable<S> source, Action<S, T> extra = null)
        {
            return source.Select(x => MapToModel(x, extra)).ToList();
        }

        public T MapToModel(S source, Action<S, T> extra = null, T destination = null)
        {
            return Map(source, extra, destination);
        }

        public S MapFromModel(T source, Action<T, S> extra = null, S destination = null)
        {
            return Map<T, S>(source, extra, destination);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, Action<TSource, TDestination> extra = null, TDestination destination = null)
            where TSource : class
            where TDestination : class

        {
            TDestination result = null;

            if (destination == null)
            {
                result = Mapper.Map<TDestination>(source);
            }
            else
            {
                result = Mapper.Map(source, destination);
            }

            extra?.Invoke(source, result);

            return result;
        }

        public virtual IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source, Action<TSource, TDestination> extra = null)
        {
            var result = new List<TDestination>();

            foreach (var item in source)
            {
                var resultItem = Mapper.Map<TSource, TDestination>(item);

                extra?.Invoke(item, resultItem);

                result.Add(resultItem);
            }

            return result;

        }
    }
}
