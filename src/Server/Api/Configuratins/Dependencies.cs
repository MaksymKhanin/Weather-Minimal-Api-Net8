using Business.PipelineBehavior;
using FluentValidation;
using MediatR;

namespace Weather_Minimal_Api.Configuratins;

public static class Dependencies
{
    public static IServiceCollection AddMediatorPipelineBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        var assembly = AppDomain.CurrentDomain.Load("Business");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
