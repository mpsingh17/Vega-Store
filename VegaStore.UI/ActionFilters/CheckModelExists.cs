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
            var modelId = context.ActionArguments
                .SingleOrDefault(x => x.Key.ToString().Contains("id")).Value as int?;

            var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

            if (modelId is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {ModelId} sent by client.", modelId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            var modelInDb = _repository.Models.GetSingleModelAsync((int)modelId, trackChanges).Result;

            if (modelInDb is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {ModelId} sent by client.", modelId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }
            
            context.HttpContext.Items.Add(nameof(modelInDb), modelInDb);

            base.OnActionExecuting(context);
        }
    }
}
