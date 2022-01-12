using AutoMapper;
using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Infrastructure.Persistence.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserEntity>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Id))
            .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));

        CreateMap<Address, AddressEntity>();

        CreateMap<UserRole, UserRoleEntity>();
        CreateMap<UserRoleEntity, UserRole>();

        CreateMap<AddressEntity, Address>()
            .ConstructUsing(x => new Address(x.Street, x.City, x.PostCode, x.Country));

        CreateMap<UserEntity, User>()
            .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => new AggregateId(x.Id)));
    }
}