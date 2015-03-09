namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Web.ViewModels.Home;

    public class HomeController : BaseController
    {

        public HomeController(IVotingSystemData data) : base (data)
        {
        }
        public ActionResult Index()
        {
            var pollCollection = new IndexViewModel()
            {
                ActivePolls =
                    this.Data.Polls.AllActive()
                    .Where(p => p.IsPublic)
                    .OrderByDescending(x => x.StartDate)
                    .Project()
                    .To<IndexPollViewModel>()
                    .Take(3)
                    .ToList(),
                ActiveVotes =
                    this.Data.Votes.AllActive()
                    .Where(v => v.IsPublic)
                    .OrderByDescending(x => x.StartDate)
                    .Project()
                    .To<IndexVoteViewModel>()
                    .Take(3)
                    .ToList(),
            };

            return this.View(pollCollection);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}