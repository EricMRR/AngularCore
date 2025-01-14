using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApplicationAPI.Controllers
{
    public class NoPropertyNamingPolicyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            if (ctx.Result is ObjectResult objectResult)
            {
                // Create options to match the CMS delivery API, along with any customizations we want for Forms.
                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    DictionaryKeyPolicy = null, //JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = null, //JsonNamingPolicy.CamelCase,

                    // Output properties in alphabetical order.
                    // Hat-tip: https://stackoverflow.com/a/72593993
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver
                    {
                        //Modifiers = { AlphabetizeProperties() },
                    },
                };
                options.Converters.Add(new JsonStringEnumConverter());
                objectResult.Formatters.Add(new SystemTextJsonOutputFormatter(options));
            }
        }
    }
}
