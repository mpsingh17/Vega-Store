using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.UI.Areas.Admin.ViewModels;
using VegaStore.UI.Areas.Admin.ViewModels.VehicleViewModels;
using VegaStore.UI.ViewModels;
using VegaStore.UI.ViewModels.VehicleViewModels;

namespace VegaStore.UI.Utility
{
    public class LinkBuilder
    {
        private readonly LinkGenerator _linkGenerator;

        public LinkBuilder(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public void GenerateVehicleLinks(IEnumerable<ListVehicleViewModel> vehiclesViewModels, HttpContext httpContext)
        {
            foreach (var vehicleVM in vehiclesViewModels)
            {
                var links = new List<LinkViewModel>
                {
                    new LinkViewModel
                    {
                        Href = _linkGenerator.GetUriByAction(httpContext, "Detail", values: new { vehicleVM.Id }),
                        Rel = "self",
                        Method = "GET"
                    },
                    new LinkViewModel
                    {
                        Href = _linkGenerator.GetUriByAction(httpContext, "Edit", values: new { vehicleVM.Id }),
                        Rel = "edit_vehicle",
                        Method = "GET"
                    },
                    new LinkViewModel
                    {
                        Href = _linkGenerator.GetUriByAction(httpContext, "Delete", values: new { vehicleVM.Id }),
                        Rel = "delete_vehicle",
                        Method = "DELETE"
                    },
                };
                
                vehicleVM.Links = links;
            }
        }
    }
}
