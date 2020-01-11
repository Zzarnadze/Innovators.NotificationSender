using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Innovators.FileService.API.ConventionsAndFilters
{
    /// <summary>
    /// Model state filter
    /// </summary>
    public class ModelStateFilter : IActionFilter
    {
        /// <summary>
        /// Triggers on action execution
        /// </summary>
        /// <param name="context">Action execution context</param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        /// <summary>
        /// Triggers on action executing context
        /// </summary>
        /// <param name="context">Action execution context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
