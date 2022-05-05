﻿using AutoMapper;
using Domain.Model;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Models;

public class ViewToDomainModelMappingProfile : Profile
{
    public ViewToDomainModelMappingProfile()
    {

        // Mapping Request to Command
        CreateMap<CompanyDb, Company>();
        CreateMap<Company, CompanyDb>();

  
        CreateMap<BranchDb, Branch>();
        CreateMap<Branch, BranchDb>();
    }
}