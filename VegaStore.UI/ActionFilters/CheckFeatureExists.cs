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
    public class CheckFeatureExists : ActionFilterAttribute
    {
        private readonly ILogger<CheckFeatureExists> _logger;
        private readonly IRepositoryManager _repository;

        public CheckFeatureExists(
            ILogger<CheckFeatureExists> logger,
            IRepositoryManager repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ActionArguments.TryGetValue("id", out object value)
                && value is int featureId)
            {
                var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

                var featureInDb = _repository.Features.GetSingleFeatureAsync(featureId, trackChanges).Result;

                if (featureInDb is null)
                {
                    _logger.LogWarning(LogEventId.Warning, "Invalid Feature ID = {FeatureId} sent by client.", featureId);
                    context.Result = new ViewResult
                    {
                        StatusCode = 404,
                        ViewName = "NotFound"
                    };
                }

                context.HttpContext.Items.Add(nameof(featureInDb), featureInDb);
            }

            base.OnActionExecuting(context);
        }
    }
}
