using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSWebApp.Models;
using WMSWebApp.ViewModels;
using WMS.Core;
using WMS.Data;
using WMS.Core.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.ResponseCompression;

namespace WMSWebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("default");



            //services.AddDbContext<AppDBContext>(c =>
            //{
            //    c.UseSqlite("Data Source=blog.db");
            //});
            //services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<WMSObjectContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";

            });
            //Initialize the mapper
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile(new ViewToDomainModelMappingProfile());

            });
            services.AddDbContext<WMSObjectContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("default")).UseLazyLoadingProxies();

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddResponseCompression();
            services.AddHttpClient();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IOTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>
                (
                    options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.User.RequireUniqueEmail = true;
                    }


                )
                .AddRoleManager<RoleManager<IdentityRole>>()
               .AddEntityFrameworkStores<WMSObjectContext>()
               .AddDefaultTokenProviders();
            //services.AddTransient<IDbContext, WMSObjectContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSingleton<IMapper>(new Mapper(config));
            services.AddSingleton<IAdoConnection>(new AdoConnection(connectionString));
            //services.AddTransient<ICompanyHelper, CompanyHelper>();
            //services.AddTransient<IBranchHelper, BranchHelper>();
            //services.AddTransient<ICustomerHelper,CustomerHelper>();
            //services.AddTransient<IItemHelper, ItemHelper>();
            //services.AddTransient<ISubItemHelper, SubItemHelper>();
            //services.AddTransient<IIntrasitHelper, IntrasitHelper>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;//6
                options.Password.RequiredUniqueChars = 1;//1

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(5);

                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/Login");
                options.SlidingExpiration = true;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSingleton<IFileProvider>( new PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot")));
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;

            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            //services.AddControllers();
            return services.ConfigureApplicationServices(Configuration, _webHostEnvironment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
