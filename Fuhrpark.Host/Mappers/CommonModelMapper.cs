using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface ICommonModelMapper<Dto, Model> : IModelMapper<Dto, Model> where Dto: CommonDto where Model: CommonModel
    {
    }
    public class CommonModelMapper<Dto, Model> : AbstractModelMapper<Dto, Model>, ICommonModelMapper<Dto, Model> where Dto : CommonDto where Model : CommonModel
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dto, Model>();
                cfg.CreateMap<Model, Dto>()
                    .ForMember(x => x.CreateDate, y => y.MapFrom(z => z.CreateDate.HasValue ? z.CreateDate.Value : DateTime.Now));
            });

            return config.CreateMapper();
        }
    }
}
