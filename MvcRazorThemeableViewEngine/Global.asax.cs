using MvcRazorThemeableViewEngine.CustomViewEngines;

namespace MvcRazorThemeableViewEngine
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        public static void RegisterViewEngine(ViewEngineCollection viewEngines)
        {
            // We do not need the default view engine
            viewEngines.Clear();

            var themeableRazorViewEngine = new ThemeableRazorViewEngine
                                               {
                                                   CurrentTheme = httpContext => httpContext.Session["theme"] as string ?? string.Empty
                                               };

            viewEngines.Add(themeableRazorViewEngine);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterViewEngine(ViewEngines.Engines);
        }
    }
}