using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Linq;

namespace Innovators.FileService.API.ConventionsAndFilters
{
    /// <summary>
    /// Version convention
    /// </summary>
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        /// <summary>
        /// Convention application
        /// </summary>
        /// <param name="controller">Controller</param>
        public void Apply(ControllerModel controller)
        {
            var apiVersionAttrs = controller.Attributes
                .Where(x => typeof(IApiVersionProvider).IsAssignableFrom(x.GetType()))
                .Select(x => (IApiVersionProvider)x);

            var apiVersion = apiVersionAttrs.SelectMany(x => x.Versions).OrderBy(x => x.MajorVersion).FirstOrDefault();
            controller.ApiExplorer.GroupName = apiVersion != null ? $"v{apiVersion}" : "";
        }
    }
}
