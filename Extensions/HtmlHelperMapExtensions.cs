using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

namespace Summit.Core.Extensions {
    public static class HtmlHelperMapExtensions {
        public static MvcHtmlString ToMapJson(this HtmlHelper html, IEnumerable<dynamic> contents) {
            var json = contents.Aggregate(string.Empty, (current, content) => current + string.Concat(ToMapJson(html, content, true).ToHtmlString(), ","));
            return new MvcHtmlString(json.TrimEnd(','));
        }

        public static MvcHtmlString ToMapJson(this HtmlHelper html, dynamic content, bool includeTooltip) {
            var json = string.Empty;
            var locationPart = content.ContentItem.LocationPart;

            if (locationPart != null) {
                var contentItem = (IContent) content.ContentItem;

                var addressFields = new List<string> {
                    locationPart.Address.Value,
                    locationPart.City.Value,
                    locationPart.ProvinceState.Value,
                    locationPart.Country.Value,
                    locationPart.Postcode.Value
                };

                var tooltipMarkup = includeTooltip ? string.Concat("<a href='", contentItem.As<IAliasAspect>().Path, "'>", contentItem.As<ITitleAspect>().Title, "</a>") : string.Empty;
                json = string.Concat("{address:\"", string.Join(",", addressFields), "\"", includeTooltip ? string.Concat(" , data: \"", tooltipMarkup, "\"") : string.Empty,  " }");
            }

            return new MvcHtmlString(json);
        }

    }
}