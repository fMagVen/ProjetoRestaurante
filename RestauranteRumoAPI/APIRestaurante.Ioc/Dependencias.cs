using APIRestaurante.Repositories.Repositories;
using APIRestaurante.Services;
using Microsoft.Extensions.DependencyInjection;

namespace APIRestaurante.Ioc
{
    public class Dependencias
    {
        public static void Injetar(IServiceCollection services)
        {
            //repos
            services.AddScoped<CardapioRepository, CardapioRepository>();
            services.AddScoped<ClienteRepository, ClienteRepository>();
            services.AddScoped<ColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<PedidoRepository, PedidoRepository>();

            //services
            services.AddScoped<CardapioService, CardapioService>();
            services.AddScoped<ClienteService, ClienteService>();
            services.AddScoped<ColaboradorService, ColaboradorService>();
            services.AddScoped<PedidoService, PedidoService>();
        }

    }
}
