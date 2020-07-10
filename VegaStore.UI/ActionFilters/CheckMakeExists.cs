using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Constants;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.Services;
using VegaStore.UI.ViewModels.ModelViewModels;

namespace VegaStore.UI.ActionFilters
{
    public class CheckMakeExists : ActionFilterAttribute
    {
        private readonly ILogger<CheckMakeExists> _logger;
        private readonly IRepositoryManager _repository;

        public CheckMakeExists(
            ILogger<CheckMakeExists> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out object value) && value is int makeId)
            {
                var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

                var makeInDb = _repository.Makes.GetSingleMakeAsync(makeId, trackChanges).Result;

                if (makeInDb is null)
                {
                    _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", makeId);
                    
                    // if AJAX request
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        var statusCodePagesFeature = context.HttpContext.Features.Get<IStatusCodePagesFeature>();
                        if (statusCodePagesFeature != null)
                            statusCodePagesFeature.Enabled = false;
                    }
                    context.Result = new NotFoundResult();
                }

                context.HttpContext.Items.Add(nameof(makeInDb), makeInDb);
            }
            
            base.OnActionExecuting(context);
        }
    }
}
