using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Penguin.Configuration.Abstractions.Interfaces;
using Penguin.Web.Configuration.Exceptions;

namespace Penguin.Web.Configuration.Attributes
{
    public sealed class RequiresConfigurationAttribute : ActionFilterAttribute, IActionFilter
    {
        public string Configuration { get; set; }

        public bool Value { get; set; }

        public RequiresConfigurationAttribute(string configuration, bool value)
        {
            this.Configuration = configuration;
            this.Value = value;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext is null)
            {
                throw new System.ArgumentNullException(nameof(filterContext));
            }

            bool ConfigParsed = bool.TryParse(filterContext.HttpContext.RequestServices.GetService<IProvideConfigurations>().GetConfiguration(this.Configuration), out bool val);

            if ((ConfigParsed && val != this.Value) || (this.Value && !ConfigParsed))
            {
                throw new FailedConfigurationCheckException();
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}