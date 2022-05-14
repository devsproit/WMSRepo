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
using WMS.Core.Infrastructure;


namespace WMS.Web.Framework.Infrastructure.Extentsion
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services,
          IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //create default file provider
            CommonHelper.DefaultFileProvider = new WMSFileProvider(hostingEnvironment);

            //create engine and configure service provider
            var engine = EngineContext.Create();
            var serviceProvider = engine.ConfigureServices(services, configuration);
            return serviceProvider;
        }
    }
}
