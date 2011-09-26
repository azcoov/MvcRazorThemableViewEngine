using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MvcRazorThemeableViewEngine.Controllers
{
    public class ThemeSwitcherController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var theme = Session["theme"] as string ?? string.Empty;

            var themePath = Server.MapPath("~/Views/Themes");
            var themeDirectory = new DirectoryInfo(themePath);

            var themes = themeDirectory.GetDirectories()
                                       .Select(d => d.Name)
                                       .Select(d => new SelectListItem { Text = d, Value = Url.Action("Switch", "ThemeSwitcher", new { theme = d }), Selected = d.Equals(theme) })
                                       .OrderBy(t => t.Text)
                                       .ToList();

            themes.Insert(0, new SelectListItem { Text = "None", Value = Url.Action("Switch", "ThemeSwitcher", new { theme = string.Empty }), Selected = theme.Length == 0 });

            return PartialView(themes);
        }

        public ActionResult Switch(string theme)
        {
            Session["theme"] = theme ?? string.Empty;

            var url = Request.UrlReferrer != null ?
                      Request.UrlReferrer.ToString() :
                      Url.Action("Index", "Home");

            return Redirect(url);
        }
    }
}