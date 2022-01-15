using System.Reflection;
using Autofac;
using AutoMapper;
using PlaygroundShared.Application.IoC;
using PlaygroundShared.Configurations;
using PlaygroundShared.Domain.IoC;
using PlaygroundShared.Domain.Shared;
using PlaygroundShared.Infrastructure.Core.IoC;
using PlaygroundShared.Infrastructure.MongoDb.IoC;
using PlaygroundShared.IntercontextCommunication.RabbitMq.IoC;
using ServiceMe.Auth.Application.Users.Commands;
using ServiceMe.Auth.Domain.Users;
using ServiceMe.Auth.Infrastructure.MongoDb.Users;
using ServiceMe.Auth.Infrastructure.Persistence.Users;
using Module = Autofac.Module;

namespace ServiceMe.Auth.Web.IoC;

public class RootModule : Module
{
    private readonly IConfiguration _configuration;

    public RootModule(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.Register(ctx => _configuration.GetSection("JwtConfig").Get<JwtConfiguration>()).As<IJwtConfiguration>().SingleInstance();
        builder.RegisterModule(new MongoDbModule(_configuration, typeof(UserMongoRepository).Assembly));
        builder.RegisterModule(new RabbitMqModule("./busConfig.json"));
        builder.RegisterModule(new DomainModule(typeof(User).Assembly));
        builder.RegisterModule(new InfrastructureModule(new []{typeof(UserMongoRepository).Assembly,typeof(UserRepository).Assembly}));
        builder.RegisterModule(new ApplicationModule(typeof(RegisterUserCommand).Assembly));
        builder.Register(ctx => new CorrelationContext()).As<ICorrelationContext>().InstancePerLifetimeScope();
        
        builder.Register(ctx =>
        {
            var assemblies = new List<Assembly>()
            {
                typeof(UserMappingProfile).Assembly
            };

            var profiles = assemblies.SelectMany(x => x.GetExportedTypes()).Where(x => x.IsAssignableTo<Profile>())
                .Select(x => (Profile) Activator.CreateInstance(x));

            var cfg = new MapperConfiguration(m =>
            {
                m.DisableConstructorMapping();
                m.AddProfiles(profiles);
            });

            return new Mapper(cfg);
        }).As<IMapper>().InstancePerLifetimeScope();
    }
}