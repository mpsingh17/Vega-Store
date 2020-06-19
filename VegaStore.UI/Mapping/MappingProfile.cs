﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;
using VegaStore.UI.ViewModels.MakeViewModels;
using VegaStore.UI.ViewModels.UserViewModels;

namespace VegaStore.UI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region ViewModel to Model
            CreateMap<SaveMakeViewModel, Make>().ReverseMap();
            #endregion

            #region Model to ViewModel
            CreateMap<Make, ListMakeViewModel>()
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(m => m.CreatedAt.ToLongDateString()));

            CreateMap<IdentityUser, ListUserViewModel>();
            #endregion
        }
    }
}
