using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;
using VegaStore.UI.ViewModels.FeatureViewModels;
using VegaStore.UI.ViewModels.MakeViewModels;
using VegaStore.UI.ViewModels.ModelViewModels;
using VegaStore.UI.ViewModels.UserViewModels;
using VegaStore.UI.ViewModels.VehicleViewModels;

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

            CreateMap<EditVehicleViewModel, Vehicle>()
                .ForMember(v => v.VehicleFeatures, opt => opt.MapFrom(vm => vm.FeatureIds.Select(fId => new VehicleFeature { FeatureId = fId })));
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

            CreateMap<Vehicle, ListVehicleViewModel>()
                .ForMember(vm => vm.Model, opt => opt.MapFrom(v => v.Model.Name))
                .ForMember(vm => vm.Price, opt => opt.MapFrom(v => v.Price.ToString("c")))
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(v => v.CreatedAt.ToLongDateString()));

            CreateMap<Vehicle, EditVehicleViewModel>()
                .ForMember(vm => vm.FeatureIds, opt => opt.MapFrom(v => v.VehicleFeatures.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, DetailVehicleViewModel>()
                .ForMember(vm => vm.Model, opt => opt.MapFrom(v => v.Model.Name))
                .ForMember(vm => vm.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(vf => vf.Feature.Name)))
                .ForMember(vm => vm.Price, opt => opt.MapFrom(v => v.Price.ToString("c")))
                .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(v => v.CreatedAt.ToLongDateString()))
                .ForMember(vm => vm.UpdatedAt, opt => opt.MapFrom(v => v.UpdatedAt.ToLongDateString()));
            #endregion
        }
    }
}
