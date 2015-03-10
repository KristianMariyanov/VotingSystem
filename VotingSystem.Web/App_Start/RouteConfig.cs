namespace VotingSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Vote with code",
                url: "Votes/VoteWithCode/{code}",
                defaults: new { controller = "Votes", action = "VoteWithCode" },
                namespaces: new[] { "VotingSystem.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "VotingSystem.Web.Controllers" });

            routes.MapRoute(
                    "Redirect",
                    "",
                    new { controller = "Home", action = "index" },
                new[] { "VotingSystem.Web.Controllers" });
        }
    }
}
