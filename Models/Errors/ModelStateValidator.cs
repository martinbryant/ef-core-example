
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ef_core_example.Models
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            // (string fieldName, ModelStateEntry entry) = context.ModelState
            //     .First(x => x.Value.Errors.Count > 0);
            // string errorSerialized = entry.Errors.First().ErrorMessage;

            // Error error         = Error.Deserialize(errorSerialized);
            // Envelope envelope   = Envelope.Error(error, fieldName);
            // var result          = new BadRequestObjectResult(envelope);

            // return result;
            throw new NotImplementedException();
        }
    }
}