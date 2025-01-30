using CryptoCurrency.BLL.Servicios.Contrato;
using CryptoCurrency.BLL.Servicios;
using CryptoCurrency.DAL.DBContext;
using CryptoCurrency.DAL.Repositorios;
using CryptoCurrency.DAL.Repositorios.Contrato;
using CryptoCurrency.Utility;
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
            service.AddAutoMapper(typeof(AutoMapperProfile));
            service.AddScoped<IRolService, RolService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IMenuService, MenuService>();
            service.AddScoped<ICriptoMonedaService, CriptoMonedaService>();
        }
    }
}
