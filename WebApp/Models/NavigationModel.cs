using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApp.Models
{
    public static class NavigationModel
    {
        private const string Underscore = "_";
        private const string Space = " ";

        //public static SmartNavigation Seed => BuildNavigation();
        //public static SmartNavigation Full => BuildNavigation(seedOnly: false);

        public static SmartNavigation Seed(HttpContext context) {
            return BuildNavigation(context, true);
        }
        public static SmartNavigation Full(HttpContext context)
        {
            return BuildNavigation(context, false);
        }

        private static SmartNavigation BuildNavigation(HttpContext context,bool seedOnly = true)
        {
            //var jsonText = File.ReadAllText("nav_bak.json");
            var jsonText = MenuHelper.GetNavMenu(context);
            var navigation = NavigationBuilder.FromJson(jsonText);
            var menu = FillProperties(navigation.Lists, seedOnly);
            return new SmartNavigation(menu);
        }

        public static SmartNavigation PwaFull(HttpContext context)
        {
            return PwaBuildNavigation(context, false);
        }

        private static SmartNavigation PwaBuildNavigation(HttpContext context, bool seedOnly = true)
        {
            //var jsonText = File.ReadAllText("nav_bak.json");
            var jsonText = MenuHelper.PwaGetNavMenu(context);
            var navigation = NavigationBuilder.FromJson(jsonText);
            var menu = FillProperties(navigation.Lists, seedOnly);
            return new SmartNavigation(menu);
        }

        private static List<ListItem> FillProperties(IEnumerable<ListItem> items, bool seedOnly, ListItem parent = null)
        {
            var result = new List<ListItem>();

            foreach (var item in items)
            {
                item.Text = item.Text ?? item.Title;
                item.Tags = string.Concat(parent?.Tags, Space, item.Title.ToLower()).Trim();

                var route = Path.GetFileNameWithoutExtension(item.Href ?? string.Empty)?.Split(Underscore);

                item.Route = route?.Length > 1 ? $"/{route.First()}/{string.Join(string.Empty, route.Skip(1))}" : item.Href;

                item.I18n = parent == null
                    ? $"nav.{item.Title.ToLower().Replace(Space, Underscore)}"
                    : $"{parent.I18n}_{item.Title.ToLower().Replace(Space, Underscore)}";

                item.Items = FillProperties(item.Items, seedOnly, item);

                if (!seedOnly || item.ShowOnSeed)
                    result.Add(item);
            }

            return result;
        }
    }
}
