namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Web.ViewModels.Candidates;
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

            return this.View(polls);
        }

        [HttpGet]
        public ActionResult Vote(int Id)
        {
            var currentVote = this.Data.Votes.GetById(Id);
            if (!currentVote.IsPublic)
            {
                this.TempData["Error"] = "You cannot vote for private Vote without identification code";
                return this.RedirectToAction("Index", "Home");
            }
            var voteWithCandidates = new VoteWithCandidatesInputModel()
            {
                Id = currentVote.Id,
                Title = currentVote.Title,
                NumberOfVotes = currentVote.NumberOfVotes,
                Candidates =
                    this.Data.Candidates.AllByVote(Id)
                    .Project()
                    .To<CandidateInputModel>()
            };

            return this.View(voteWithCandidates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteWithCandidatesInputModel model)
        {
            if (model != null)
            {
                var checkedCandidates = model.Candidates.Where(x => x.IsChecked);
                var currentVote = this.Data.Votes.GetById(model.Id);
                if (checkedCandidates.Count() != currentVote.NumberOfVotes)
                {
                    this.ModelState.AddModelError("Candidate", "does not match with restricted number of votes");
                    return this.View(model);
                }
                foreach (var candidate in model.Candidates)
                {
                    if (candidate.IsChecked)
                    {
                        var currentCandidate = this.Data.Candidates.GetById(candidate.Id);
                        if (currentVote.Candidates.Contains(currentCandidate))
                        {
                            currentCandidate.VoteCount++;

                            this.Data.Candidates.Update(currentCandidate);
                            this.Data.SaveChanges();
                        }
                        else
                        {
                            this.ModelState.AddModelError("Candidate", "does not match with the Vote");
                        }

                    }
                }

                this.RedirectToAction("All", "Votes", new { id = model.Id, Area = string.Empty });
            }

            return this.View(model);
        }

        public ActionResult Details(int Id)
        {

            return this.View();
        }
    }
}