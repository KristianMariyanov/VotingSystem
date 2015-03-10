namespace VotingSystem.Web.Areas.User.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Models;
    using VotingSystem.Web.Areas.User.ViewModels;
    using VotingSystem.Web.ViewModels.Candidates;
    using VotingSystem.Web.ViewModels.Votes;

    public class CandidatesController : UserBaseController
    {
        // GET: User/Candidates
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

        public ActionResult Moderate(int id)
        {
            var currentVote = this.Data.Votes.GetById(id);

            var voteInfo = new VoteWithCandidatesInputModel()
                                 {
                                     Id = currentVote.Id,
                                     Title = currentVote.Title,
                                     NumberOfVotes = currentVote.NumberOfVotes,
                                     Candidates =
                                         this.Data.Candidates.AllByVote(currentVote.Id)
                                         .Project()
                                         .To<CandidateInputModel>()
                                 };


            return this.View(voteInfo);
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            var candidateModel = new CandidateViewModel() { VoteId = id };
            return this.View(candidateModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CandidateViewModel candidate)
        {
            if (candidate != null && this.ModelState.IsValid)
            {
                candidate.VoteCount = 0;
                var candidateToDb = Mapper.DynamicMap<Candidate>(candidate);

                this.Data.Candidates.Add(candidateToDb);
                this.Data.SaveChanges();

                return this.RedirectToAction("All", new { id = candidate.VoteId });
            }

            return this.View(candidate);
        }
    }
}