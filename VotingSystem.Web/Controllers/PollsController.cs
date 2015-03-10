namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

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

        public ActionResult Details(int id)
        {
            return this.View();
        }
    }
}