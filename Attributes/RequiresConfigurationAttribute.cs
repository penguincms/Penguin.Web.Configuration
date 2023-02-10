using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Penguin.Configuration.Abstractions.Interfaces;
using Penguin.Web.Configuration.Exceptions;

namespace Penguin.Web.Configuration.Attributes
{
    public sealed class RequiresConfigurationAttribute : ActionFilterAttribute, IActionFilter
    {
        public string Configuration { get; internal set; }

        public bool Value { get; internal set; }

        public RequiresConfigurationAttribute(string configuration, bool value)
        {
            Configuration = configuration;
            Value = value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }

            bool ConfigParsed = bool.TryParse(context.HttpContext.RequestServices.GetService<IProvideConfigurations>().GetConfiguration(Configuration), out bool val);

            if ((ConfigParsed && val != Value) || (Value && !ConfigParsed))
            {
                throw new FailedConfigurationCheckException();
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}