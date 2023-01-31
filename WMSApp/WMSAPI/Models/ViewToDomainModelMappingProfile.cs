using AutoMapper;
using Domain.Model;
using WMSAPI.ViewModels;
using Domain.Model.Masters;
using WMS.Data;
using System.Linq;
using System.Collections.Generic;
namespace WMSAPI.Models
{



    public class ViewToDomainModelMappingProfile : Profile
    {
        public ViewToDomainModelMappingProfile()
        {

            // Mapping Request to Command
            CreateMap<CompanyDb, Company>();
            CreateMap<Company, CompanyDb>();


            CreateMap<Branch, BranchModel>()
                .ForMember(model => model.Warehouses, options => options.Ignore());

            CreateMap<BranchModel, Branch>()
                .ForMember(entity => entity.BranchWiseWarehouses, options => options.Ignore());








            CreateMap<CustomerDb, Customer>();
            CreateMap<Customer, CustomerDb>();

            CreateMap<ItemDb, Item>();
            CreateMap<Item, ItemDb>();

            CreateMap<SubItemDb, SubItem>();
            CreateMap<SubItem, SubItemDb>();

            // Ware house

            CreateMap<Warehouse, WarehouseModel>();
            CreateMap<WarehouseModel, Warehouse>();
            CreateMap<WarehouseZoneAreaModel, WarehouseZone>();
            CreateMap<WarehouseZone, WarehouseZoneAreaModel>();
            CreateMap<WarehouseArea, WarehouseZoneArea>();
            CreateMap<WarehouseZoneArea, WarehouseArea>();

            CreateMap<RegisterViewModel, UserProfile>();
            CreateMap<UserProfile, RegisterViewModel>();
        }
    }
}


