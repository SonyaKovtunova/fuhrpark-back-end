using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Fuhrpark.Host.Mappers
{
    public interface IModelMapperFactory
    {
        T GetMapper<T>() where T : IModelMapper;
    }

    public class ModelMapperFactory : IModelMapperFactory
    {
        public ModelMapperFactory(IUnityContainer container)
        {
            Container = container;
        }

        public IUnityContainer Container { get; private set; }

        public T GetMapper<T>() where T : IModelMapper
        {
            return Container.Resolve<T>();
        }
    }
}
