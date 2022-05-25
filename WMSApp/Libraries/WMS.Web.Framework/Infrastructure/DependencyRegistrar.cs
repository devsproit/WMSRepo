using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WMS.Core.Infrastructure.DependencyManagement;
using WMS.Data;
using WMS.Core.Data;
#region Service
using Application.Services;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Infrastructure;
using Application.Services.WarehouseMaster;
using Application.Services.GRN;
using Application.Services.Master;
#endregion


namespace WMS.Web.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private IServiceProvider _serviceProvider { get; set; }
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(context => new WMSObjectContext(context.Resolve<DbContextOptions<WMSObjectContext>>()))
               .As<IDbContext>().InstancePerLifetimeScope();
            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<CompanyHelper>().As<ICompanyHelper>().InstancePerLifetimeScope();
            builder.RegisterType<BranchHelper>().As<IBranchHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerHelper>().As<ICustomerHelper>().InstancePerLifetimeScope();
            builder.RegisterType<ItemHelper>().As<IItemHelper>().InstancePerLifetimeScope();
            builder.RegisterType<SubItemHelper>().As<ISubItemHelper>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseService>().As<IWarehouseService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodReceivedNoteMasterService>().As<IGoodReceivedNoteMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerLifetimeScope();
        }

        public int Order => 0;
    }
}
