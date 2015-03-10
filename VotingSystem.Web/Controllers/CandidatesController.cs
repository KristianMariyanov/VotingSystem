namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Web.Areas.User.ViewModels;

    public class CandidatesController : BaseController
    {
        // GET: Candidates
        public CandidatesController(IVotingSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All(int id)
        {
            var candidates = this.Data
                .Candidates
                .AllByVote(id)
                .Project()
                .To<CandidateViewModel>()
                .ToList();

            return this.View(candidates);
        }
    }
}