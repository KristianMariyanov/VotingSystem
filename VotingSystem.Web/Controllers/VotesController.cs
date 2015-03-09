namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Web.ViewModels.Votes;

    public class VotesController : BaseController
    {
        // GET: Votes
        public VotesController(IVotingSystemData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var polls = this.Data
                .Votes
                .AllActive()
                .Where(v => v.IsPublic)
                .Project()
                .To<PublicActiveVotesViewModel>()
                .ToList();

            return View(polls);
        }

        public ActionResult Details(int Id)
        {

            return View();
        }
    }
}