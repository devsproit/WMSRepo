﻿using System;
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
using Microsoft.AspNetCore.Http;
using Application.Services.PO;
using Domain.Model.PO;
using Application.Services.PS;
using Application.Services.Invoice;
using Application.Services.StockMgnt;
using Application.Services.Security;
using Application.Services.SRN;
using Application.Services.Report;
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
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyHelper>().As<ICompanyHelper>().InstancePerLifetimeScope();
            builder.RegisterType<BranchHelper>().As<IBranchHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerHelper>().As<ICustomerHelper>().InstancePerLifetimeScope();
            builder.RegisterType<ItemHelper>().As<IItemHelper>().InstancePerLifetimeScope();
            builder.RegisterType<SubItemHelper>().As<ISubItemHelper>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseService>().As<IWarehouseService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodReceivedNoteMasterService>().As<IGoodReceivedNoteMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerLifetimeScope();
            builder.RegisterType<IntrasitHelper>().As<IIntrasitHelper>().InstancePerLifetimeScope();
            builder.RegisterType<IntrasitService>().As<IIntrasitService>().InstancePerLifetimeScope();
            builder.RegisterType<PurchaseOrder>().As<IPurchaseOrder>().InstancePerLifetimeScope();
            builder.RegisterType<SenderCompanyHelper>().As<ISenderCompany>().InstancePerLifetimeScope();
            builder.RegisterType<SalePo>().As<ISalePo>().InstancePerLifetimeScope();
            builder.RegisterType<StockTransferPo>().As<IStockTransferPo>().InstancePerLifetimeScope();
            builder.RegisterType<SrnPo>().As<ISrnPo>().InstancePerLifetimeScope();
            builder.RegisterType<ServiceOrderPo>().As<IServiceOrderPo>().InstancePerLifetimeScope();

            builder.RegisterType<PickSlipService>().As<IPickSlipService>().InstancePerLifetimeScope();
            builder.RegisterType<TempPickSlipDetailsService>().As<ITempPickSlipDetailsService>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>().InstancePerLifetimeScope();
            builder.RegisterType<ItemStockService>().As<IItemStockService>().InstancePerLifetimeScope();
            builder.RegisterType<UserBranchMappingService>().As<IUserBranchMappingService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionMasterService>().As<IPermissionMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionRoleMappingService>().As<IPermissionRoleMappingService>().InstancePerLifetimeScope();
            builder.RegisterType<SRNNoteMasterService>().As<ISRNNoteMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<VenderVehicleService>().As<IVenderVehicleService>().InstancePerLifetimeScope();
            builder.RegisterType<DispatchService>().As<IDispatchService>().InstancePerLifetimeScope();
            builder.RegisterType<SubItemWarehouseMappingService>().As<ISubItemWarehouseMappingService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();

        }

        public int Order => 0;
    }
}
