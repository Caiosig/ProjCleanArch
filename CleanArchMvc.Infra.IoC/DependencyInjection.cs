using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //Registro das classes de serviços padrões
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            //Registrando os serviços de Autenticação e roles
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            //Registrando o automapper do DomainDTO
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            //Adicionado o services para a parte de autenticação via ferramenta identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Configurando os cookies da aplicação
            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");

            //Referenciando os Handlers da camada de aplicação para utilização do MediatR
            var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(myHandlers);
            });

            return services;
        }
    }
}
