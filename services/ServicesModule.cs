using Autofac;
using services.services;

namespace services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CatalogBrandService>();
        }
    }
}
