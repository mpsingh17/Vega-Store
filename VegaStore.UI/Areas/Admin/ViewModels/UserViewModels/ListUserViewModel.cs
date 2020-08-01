using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.UI.Areas.Admin.ViewModels.UserViewModels
{
    public class ListUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }

    }
}
