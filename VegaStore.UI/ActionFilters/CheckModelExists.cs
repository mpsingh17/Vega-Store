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
using VegaStore.Core.Services;

namespace VegaStore.UI.ActionFilters
{
    public class CheckModelExists : ActionFilterAttribute
    {
        private readonly ILogger<CheckModelExists> _logger;
        private readonly IRepositoryManager _repository;

        public CheckModelExists(
            ILogger<CheckModelExists> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out object value) && value is int modelId)
            {
                var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

                var modelInDb = _repository.Models.GetSingleModelAsync(modelId, trackChanges).Result;

                if (modelInDb is null)
                {
                    _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {ModelId} sent by client.", modelId);
                    
                    // if AJAX request
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        var statusCodePagesFeature = context.HttpContext.Features.Get<IStatusCodePagesFeature>();
                        if (statusCodePagesFeature != null)
                            statusCodePagesFeature.Enabled = false;
                    }
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.Result = new NotFoundResult();
                }

                context.HttpContext.Items.Add(nameof(modelInDb), modelInDb);
            }

            base.OnActionExecuting(context);
        }
    }
}
