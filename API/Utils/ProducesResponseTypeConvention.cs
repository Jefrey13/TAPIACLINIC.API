using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Utils
{
    /// <summary>
    /// Convención para agregar automáticamente códigos de estado de respuesta
    /// a todos los endpoints de la API.
    /// </summary>
    public class ProducesResponseTypeConvention : IApplicationModelConvention
    {
        /// <summary>
        /// Aplica los códigos de respuesta 200, 401 y 403 a cada acción en los controladores.
        /// </summary>
        /// <param name="application">El modelo de aplicación.</param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    // Agrega los códigos de respuesta a cada acción
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status201Created));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status202Accepted));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status403Forbidden));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status408RequestTimeout));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                    action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                }
            }
        }
    }
}
