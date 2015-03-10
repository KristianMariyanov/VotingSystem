namespace VotingSystem.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;

    using VotingSystem.Data;
    using VotingSystem.Web.ViewModels.Polls;

    public class PollsController : BaseController
    {

        public PollsController(IVotingSystemData data) : base(data)
        {
        }
        // GET: Polls
        public ActionResult All()
        {
            var polls = this.Data
                .Polls
                .AllActive()
                .Where(p => p.IsPublic)
                .Project()
                .To<PublicActivePollsViewModel>()
                .ToList();

            return this.View(polls);
        }

        public ActionResult Details(int Id)
        {

            return View();
        }
    }
}