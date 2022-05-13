using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace WMS.Web.Framework.Infrastructure.Extentsion
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceProvider ConfigurationApplicationService(this IServiceCollection service,IConfiguration configuration)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(service);
            var serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
            return serviceProvider;
        }
    }
}
