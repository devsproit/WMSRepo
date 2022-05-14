using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WMS.Core.Infrastructure
{
    public interface IWMSStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        void Configure(IApplicationBuilder application);

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        /// 
        int Order { get; }

    }
}
