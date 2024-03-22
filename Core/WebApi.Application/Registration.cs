using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Behaviours;
using WebApi.Application.Exceptions;

namespace WebApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(assembly)); // Features klasoru altinda olan tum Mediatr islemlerini kendi otomatik tanimlayacak.

            services.AddTransient<ExceptionMiddleware>();

            services.AddValidatorsFromAssembly(assembly); // Fluent Valdiation kullanimi icin servis kaydi
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr"); // Fluent Validation turkcelestirilmesi

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
        }
    }
}
