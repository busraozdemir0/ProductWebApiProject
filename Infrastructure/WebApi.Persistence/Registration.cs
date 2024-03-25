using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces.Repositories;
using WebApi.Application.Interfaces.UnitOfWorks;
using WebApi.Domain.Entities;
using WebApi.Persistence.Context;
using WebApi.Persistence.Repositories;
using WebApi.Persistence.UnitOfWorks;

namespace WebApi.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>)); // Alanlar generic oldugunda typeof anahtar sozcugu kullanilir!!
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>)); 

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Identity yapilandirmasi
            services.AddIdentityCore<User>(opt=>
            {
                opt.Password.RequireNonAlphanumeric = false; // Alfabetik deger girmedigimizde veren hatayi devre disi birakiyoruz
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
