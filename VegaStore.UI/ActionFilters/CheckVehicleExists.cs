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
            var vehicleId = context.ActionArguments
                .SingleOrDefault(x => x.Key.Contains("id")).Value as int?;

            var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

            if (vehicleId is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Vehicle ID = {VehicleId} sent by client.", vehicleId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            var vehicleInDb = _repository.Vehicles.GetSingleVehicleAsync((int)vehicleId, includeRelated: true, trackChanges).Result;

            if (vehicleInDb is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Vehicle ID = {VehicleId} sent by client.", vehicleId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            context.HttpContext.Items.Add(nameof(vehicleInDb), vehicleInDb);

            base.OnActionExecuting(context);
        }
    }
}
