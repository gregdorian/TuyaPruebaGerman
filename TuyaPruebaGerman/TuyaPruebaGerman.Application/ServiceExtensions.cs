using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TuyaPruebaGerman.Application.Behaviours;
using TuyaPruebaGerman.Application.Helpers;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IModelHelper, ModelHelper>();
            //services.AddScoped<IMockData, MockData>();
        }
    }
}