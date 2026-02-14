using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using InternetShop.Data;
using InternetShop.Service;
using System.Text;

namespace InternetShop.Api.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IRequestHandler<>))
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime()
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            services.AddDbContext<InternetShopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("InternetShop"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(t =>
            {
                t.ApiVersionReader = new UrlSegmentApiVersionReader();
                t.ReportApiVersions = true;
            });

            services.AddSwaggerGen();

            return services;
        }
    }
}
