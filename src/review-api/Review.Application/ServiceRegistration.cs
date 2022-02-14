using System;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ICommandSender, CommandSender>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddHttpClient("ArticleClient", configureClient: client =>
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ISDOCKER")))
                client.BaseAddress = new Uri(configuration.GetSection("GatewayUrl:Local").Value);
            else
                client.BaseAddress = new Uri(configuration.GetSection("GatewayUrl:Docker").Value);
        });
        services.AddTransient<ArticleService>();

        return services;
    }
}