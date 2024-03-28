using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Bases;
using WebApi.Application.Behaviours;
using WebApi.Application.Exceptions;
using WebApi.Application.Features.Products.Rules;

namespace WebApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(assembly)); // Features klasoru altinda olan tum Mediatr islemlerini kendi otomatik tanimlayacak.

            services.AddTransient<ExceptionMiddleware>();
            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules)); // Bu assembly'de olan BaseRules'lari bulacak (Rules'larin hepsi BaseRules'dan turetildigi icin)

            services.AddValidatorsFromAssembly(assembly); // Fluent Valdiation kullanimi icin servis kaydi
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr"); // Fluent Validation turkcelestirilmesi

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehaviour<,>));
        }

        private static IServiceCollection AddRulesFromAssemblyContaining
            (this IServiceCollection services, Assembly assembly, Type type)  // assembly => o an icinde bulundugumuz katman/proje (WebApi.Application)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList(); // type != t => tipi BaseRules'in kendisi olmamasi icin.

            foreach (var item in types)
                services.AddTransient(item);

            return services;
        }
    }
}
