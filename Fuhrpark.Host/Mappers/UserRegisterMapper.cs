using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface IUserRegisterMapper: IModelMapper<AppUserDto, UserRegisterModel>
    {
    }
    public class UserRegisterMapper : AbstractModelMapper<AppUserDto, UserRegisterModel>, IUserRegisterMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUserDto, UserRegisterModel>();
                cfg.CreateMap<UserRegisterModel, AppUserDto>();
            });

            return config.CreateMapper();
        }
    }
}
