using AutoMapper;
using Newtonsoft.Json;
using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;
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

        CreateMap<IDomainEvent, UserEventEntity>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
            .ForMember(x => x.AggregateId, opt => opt.MapFrom(x => x.Id.ToGuid()))
            .ForMember(x => x.Event, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x)))
            .ForMember(x => x.CorrelationId, opt => opt.MapFrom(x => x.CorrelationId))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(x => x.EventType, opt => opt.MapFrom(x => x.GetType().FullName));

    }
}