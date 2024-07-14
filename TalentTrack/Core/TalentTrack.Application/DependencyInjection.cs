using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TalentTrack.Application.Behaviors;
using TalentTrack.Application.Helpers;

namespace TalentTrack.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.Configure<ApiBehaviorOptions>(x => { x.SuppressModelStateInvalidFilter = true; });

        // Register AutoMapper with the MappingProfile
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
