using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Constants;
using VegaStore.Core.Repositories;
using VegaStore.UI.ViewModels.ModelViewModels;

namespace VegaStore.UI.ActionFilters
{
    public class CheckMakeOfModelExists : ActionFilterAttribute
    {
        private readonly ILogger<CheckMakeOfModelExists> _logger;
        private readonly IRepositoryManager _repository;

        public CheckMakeOfModelExists(
            ILogger<CheckMakeOfModelExists> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? makeId = null;

            if (context.ActionArguments.TryGetValue("vm", out object value)
                && value is SaveModelViewModel vm)
            {
                makeId = vm.MakeId;
            }

            var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

            if (makeId is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", makeId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            var makeInDb = _repository.Makes.GetSingleMakeAsync((int)makeId, trackChanges).Result;

            if (makeInDb is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", makeId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
