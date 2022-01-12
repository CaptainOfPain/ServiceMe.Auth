using Autofac;
using PlaygroundShared.Configurations;
using PlaygroundShared.Infrastructure.MongoDb.IoC;
using PlaygroundShared.IntercontextCommunication.RabbitMq.IoC;

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

        builder.Register(ctx => _configuration.Get<JwtConfiguration>()).As<IJwtConfiguration>().SingleInstance();
        builder.RegisterModule(new MongoDbModule(_configuration));
        builder.RegisterModule(new RabbitMqModule("./busConfig.json"));
        
    }
}