using Anthill.Common.Services;
using AutoMapper;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Mappers
{
    public class AppUserMapper : AbstractMapper<AppUser, AppUserDto>, IAppUserMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, AppUserDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                    .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
                    .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                    .ForMember(x => x.Password, y => y.MapFrom(z => z.Password))
                    .ForMember(x => x.CreateDate, y => y.MapFrom(z => z.CreateDate))
                    .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsActive))
                    .ForMember(x => x.Mobile, y => y.MapFrom(z => z.Mobile))
                    .ForMember(x => x.RefreshToken, y => y.MapFrom(z => z.RefreshToken));

                cfg.CreateMap<AppUserDto, AppUser>().ForAllMembers(opt => opt.Condition((source, destination, sourceMember, destMember) => (sourceMember != null)));
            });


            return config.CreateMapper();
        }
    }
}
