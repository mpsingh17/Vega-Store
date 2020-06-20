﻿using Microsoft.AspNetCore.Mvc;
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
            var makeId = context.ActionArguments
                .SingleOrDefault(x => x.Key.ToString().Contains("id")).Value as int?;

            var trackChanges = context.HttpContext.Request.Method.Contains("POST") == true;

            if(makeId is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", makeId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            var makeInDb = _repository.Makes.GetSingleMakeAsync((int)makeId, trackChanges).Result;

            if(makeInDb is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", makeId);
                context.Result = new ViewResult
                {
                    StatusCode = 404,
                    ViewName = "NotFound"
                };
            }

            context.HttpContext.Items.Add(nameof(makeInDb), makeInDb);

            base.OnActionExecuting(context);
        }
    }
}
