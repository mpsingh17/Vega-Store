using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.TagHelpers
{
    public class LiTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string CurrentController { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var controllerName = ViewContext?.RouteData?.Values["controller"]?.ToString();

            if (controllerName != null && CurrentController == controllerName)
                output.Attributes.SetAttribute("class", "nav-item active");
        }
    }
}
