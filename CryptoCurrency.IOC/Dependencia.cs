using CryptoCurrency.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoCurrency.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<PruebaBgContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IVentaRepository, VentaRepository>();
            service.AddAutoMapper(typeof(AutoMapperProfile));
            service.AddScoped<IRolService, RolService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<ICategoriaService, CategoriaService>();
            service.AddScoped<IProductoService, ProductoService>();
            service.AddScoped<IVentaService, VentaService>();
            service.AddScoped<IDashBoardService, DashBoardService>();
            service.AddScoped<IMenuService, MenuService>();
        }
    }
}
