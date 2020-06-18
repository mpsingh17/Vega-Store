using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.UI.Helpers;

namespace VegaStore.UI.ActionFilters
{
    public class ImportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            var serialisedModelState = controller?.TempData[Key] as string;

            if (serialisedModelState != null)
            {
                if (context.Result is ViewResult)
                {
                    var modelState = ModelStateHelpers.DeserializeModelState(serialisedModelState);

                    context.ModelState.Merge(modelState);
                }
                else
                {
                    controller.TempData.Remove(Key);
                }
            }
            
            base.OnActionExecuted(context);
        }
    }
}
