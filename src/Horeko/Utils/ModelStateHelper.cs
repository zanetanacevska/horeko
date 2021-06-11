using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace Horeko.Utils
{
    public static class ModelStateHelper
    {
        public static Dictionary<string, List<string>> ToDictionary(ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, List<string>>();

            foreach (var kvp in modelState)
            {
                var messages = new List<string>();
                foreach (var error in kvp.Value.Errors)
                {
                    messages.Add(error.ErrorMessage);
                }

                errors.Add(kvp.Key, messages);
            }

            return errors;
        }
    }
}