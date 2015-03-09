namespace VotingSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using VotingSystem.Data;
    using VotingSystem.Models;

    public abstract class UserBaseController : Controller
    {
        public UserBaseController(IVotingSystemData data)
        {
            this.Data = data;
        }

        protected  User CurrentUser { get; set; }

        protected IVotingSystemData Data { get; set; }



        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.Data.Users
                                   .All()
                                   .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}