using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Constants;
using VegaStore.Core.Repositories;

namespace VegaStore.UI.ActionFilters
{
    public class CheckVehicleExists : ActionFilterAttribute
    {
        private readonly ILogger<CheckVehicleExists> _logger;
        private readonly IRepositoryManager _repository;

        public CheckVehicleExists(
            ILogger<CheckVehicleExists> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out object value) && value is int vehicleId)
            {
                var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

                var vehicleInDb = _repository.Vehicles
                    .GetSingleVehicleAsync(vehicleId, includeRelated: true, trackChanges).Result;

                _logger.LogInformation($"vehicle ID is: {vehicleId}");

                if (vehicleInDb is null)
                {
                    _logger.LogWarning(LogEventId.Warning, "Invalid Vehicle ID = {VehicleId} sent by client.", vehicleId);
                    
                    // if AJAX request
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        var statusCodePagesFeature = context.HttpContext.Features.Get<IStatusCodePagesFeature>();
                        if (statusCodePagesFeature != null)
                            statusCodePagesFeature.Enabled = false;
                    }
                    context.Result = new NotFoundResult();
                }

                context.HttpContext.Items.Add(nameof(vehicleInDb), vehicleInDb);
            }
            else
            {
                context.Result = new NotFoundResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
