

using Summit.Core.Attributes;

namespace Summit.Core.Filters
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Orchard.Mvc.Filters;
    using Orchard.Themes;


    /// <summary>The theme conditional filter.</summary>
    public class ThemeConditionalFilter : FilterProvider, IActionFilter, IResultFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            var attribute = GetThemedAttribute(filterContext.ActionDescriptor);
    
            if (attribute != null && CheckAjax(attribute, filterContext.HttpContext.Request.IsAjaxRequest())) {
                Apply(filterContext.RequestContext);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {}

        public void OnResultExecuting(ResultExecutingContext filterContext) {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {}

        public static void Apply(RequestContext context) {
            // the value isn't important
            context.HttpContext.Items[typeof(ThemeFilter)] = null;
        }

        public static bool IsApplied(RequestContext context) {
            return context.HttpContext.Items.Contains(typeof(ThemeFilter));
        }

        private static bool CheckAjax(ThemedConditionalAttribute attribute, bool isAjax)
        {
            var apply = true;

            if (isAjax && attribute != null)
            {
                if (attribute.ExcludeAjaxRequests)
                {
                    apply = false;
                }
            }

            return apply;
        }

        private static ThemedConditionalAttribute GetThemedAttribute(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof (ThemedConditionalAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(ThemedConditionalAttribute), true))
                .OfType<ThemedConditionalAttribute>()
                .FirstOrDefault();
        }
    }
}
