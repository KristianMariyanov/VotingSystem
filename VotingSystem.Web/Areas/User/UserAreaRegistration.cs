namespace VotingSystem.Web.Areas.User
{
    using System.Web.Mvc;

    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            context.MapRoute(
                    "Home",
                    "",
                    new { controller = "Home", action = "index" },
                new[] { "VotingSystem.Web.Controllers" });
        }
    }
}