using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;
using VegaStore.UI.ViewModels.FeatureViewModels;
using VegaStore.UI.ViewModels.MakeViewModels;
using VegaStore.UI.ViewModels.ModelViewModels;
using VegaStore.UI.ViewModels.UserViewModels;

namespace VegaStore.UI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region ViewModel to Model
            CreateMap<SaveMakeViewModel, Make>().ReverseMap();

            CreateMap<CreateModelViewModel, Model>();
            CreateMap<EditModelViewModel, Model>();

            CreateMap<CreateFeatureViewModel, Feature>();
            CreateMap<EditFeatureViewModel, Feature>();
            #endregion

            #region Model to ViewModel
            CreateMap<Make, ListMakeViewModel>()
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(m => m.CreatedAt.ToLongDateString()));

            CreateMap<IdentityUser, ListUserViewModel>();

            CreateMap<Model, ListModelViewModel>()
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(m => m.CreatedAt.ToLongDateString()))
                .ForMember(vm => vm.Make, opt => opt.MapFrom(m => m.Make.Name));

            CreateMap<Feature, ListFeatureViewModel>()
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(f => f.CreatedAt.ToLongDateString()));
            CreateMap<Feature, EditFeatureViewModel>();
            #endregion
        }
    }
}
