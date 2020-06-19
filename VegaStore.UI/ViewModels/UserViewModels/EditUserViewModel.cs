using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.UI.ViewModels.UserViewModels
{
    public class EditUserViewModel
    {
        public IList<string> Roles { get; set; }
        public IEnumerable<SelectListItem> RoleSLIs { get; set; }
    }
}
