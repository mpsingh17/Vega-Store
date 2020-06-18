using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.UI.Helpers;

namespace VegaStore.UI.ActionFilters
{
    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(!context.ModelState.IsValid)
            {
                if (
                    context.Result is RedirectResult ||
                    context.Result is RedirectToRouteResult ||
                    context.Result is RedirectToActionResult)
                {
                    Controller controller = context.Controller as Controller;

                    if (controller != null && context.ModelState != null)
                    {
                        var modelState = ModelStateHelpers.SerialiseModelState(context.ModelState);
                        controller.TempData[Key] = modelState;
                    }
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
