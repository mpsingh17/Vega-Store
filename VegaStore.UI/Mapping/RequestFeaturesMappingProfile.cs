using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.DbQueryFeatures;
using VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels;
using VegaStore.UI.ViewModels.RequestFeatureViewModels;

namespace VegaStore.UI.Mapping
{
    public class RequestFeaturesMappingProfile : Profile
    {
        public RequestFeaturesMappingProfile()
        {
            #region Public Request Parameter Objects to Db Query Objects.
            CreateMap<PublicVehicleRequestParametersVM, VehicleQueryParameters>()
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => (src.Start / src.Length) + 1));
            #endregion

            #region Admin Request Parameter Objects to Db Query Objects.
            CreateMap<VehicleRequestParametersVM, VehicleQueryParameters>()
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => (src.Start / src.Length) + 1))
                .ForMember(dest => dest.SearchTerm, opt => opt.MapFrom(src => src.Search.Value));
            #endregion
        }
    }
}
