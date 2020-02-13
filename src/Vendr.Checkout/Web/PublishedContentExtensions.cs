﻿using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;
using Vendr.Core.Models;
using Umbraco.Core;
using System.Linq;

using Constants = Vendr.Core.Constants;

namespace Vendr.Checkout.Web
{
    public static class PublishedContentExtensions
    {
        public static string GetMetaTitle(this IPublishedContent content)
        {
            var storeName = content.Value<string>("vendrStoreName", fallback:Fallback.ToAncestors);
            var pageTitle = content.Value<string>("pageTitle");

            if (!pageTitle.IsNullOrWhiteSpace())
                return $"{pageTitle} | {storeName}";

            return $"{content.Name} | {storeName}";
        }

        public static StoreReadOnly GetStore(this IPublishedContent content)
        {
            return content.Value<StoreReadOnly>(Constants.Properties.StorePropertyAlias, fallback:Fallback.ToAncestors);
        }

        public static IPublishedContent GetCheckoutPage(this IPublishedContent content)
        {
            return content.AncestorOrSelf("vendrCheckoutCheckoutPage");
        }

        public static IPublishedContent GetCheckoutBackPage(this IPublishedContent content)
        {
            return GetCheckoutPage(content).Value<IPublishedContent>("vendrCheckoutBackPage");
        }

        public static string GetThemeColor(this IPublishedContent content)
        {
            var themeColor = GetCheckoutPage(content).Value("vendrThemeColor", defaultValue: "000000");

            return VendrCheckoutConstants.ColorMap[themeColor];
        }

        public static IPublishedContent GetPreviousPage(this IPublishedContent content)
        {
            return content.Parent.Children.TakeWhile(x => !x.Id.Equals(content.Id)).LastOrDefault();
        }

        public static IPublishedContent GetNextPage(this IPublishedContent content)
        {
            return content.Parent.Children.SkipWhile(x => !x.Id.Equals(content.Id)).Skip(1).FirstOrDefault();
        }

    }
}
