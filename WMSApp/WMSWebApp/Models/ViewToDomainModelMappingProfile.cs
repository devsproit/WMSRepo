using AutoMapper;
using Domain.Model;
using WMSWebApp.ViewModels;
using Domain.Model.Masters;
using WMS.Data;
namespace WMSWebApp.Models
{



    public class ViewToDomainModelMappingProfile : Profile
    {
        public ViewToDomainModelMappingProfile()
        {

            // Mapping Request to Command
            CreateMap<CompanyDb, Company>();
            CreateMap<Company, CompanyDb>();


            CreateMap<BranchDb, Branch>();
            CreateMap<Branch, BranchDb>();

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


