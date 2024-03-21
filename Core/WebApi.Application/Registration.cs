using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
