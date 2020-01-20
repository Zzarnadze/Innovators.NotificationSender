using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovators.NotificationSender.Api.ConventionsAndFilters
{
    /// <summary>
    /// RemoveApiVersionFromParamsOperationFilter class
    /// </summary>
    public class RemoveApiVersionFromParamsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Removes api version from params
        /// </summary>
        /// <param name="operation">Operation</param>
        /// <param name="context">Operation filter context</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParam = operation.Parameters.FirstOrDefault(x => x.Name == "version");

            if (versionParam != null) operation.Parameters.Remove(versionParam);
        }
    }
}
