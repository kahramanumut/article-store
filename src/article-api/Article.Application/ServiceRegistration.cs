using System;
using System.Reflection;
using Article.Application.HttpServices;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Article.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRequestSender, RequestSender>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddHttpClient("ReviewClient", configureClient: client =>
            {
                if(String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ISDOCKER")))
                    client.BaseAddress = new Uri(configuration.GetSection("GatewayUrl:Local").Value);
                else
                    client.BaseAddress = new Uri(configuration.GetSection("GatewayUrl:Docker").Value);
            });
            services.AddTransient<ReviewService>();

            return services;
        }
    }

}