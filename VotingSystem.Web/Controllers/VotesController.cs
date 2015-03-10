namespace VotingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Models;
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
        public ActionResult VoteWithCode(string code)
        {
            var codeInDb = this.Data.IdentificatonCodes.GetById(code);
            if (codeInDb == null || codeInDb.VoteId == null || codeInDb.Used)
            {
                this.TempData["Error"] = "The Identification code is invalid ";
                return this.RedirectToAction("Index", "Home");
            }

            var currentVote = this.Data.Votes.GetById(codeInDb.VoteId);
            var voteWithCandidates = this.GetInputModelByVote(currentVote, code);

            return this.View("Vote", voteWithCandidates);
        }

        [HttpGet]
        public ActionResult Vote(int id)
        {
            var currentVote = this.Data.Votes.GetById(id);
            if (!currentVote.IsPublic)
            {
                this.TempData["Error"] = "You cannot vote for private Vote without identification code";
                return this.RedirectToAction("Index", "Home");
            }

            var voteWithCandidates = this.GetInputModelByVote(currentVote);

            return this.View(voteWithCandidates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteWithCandidatesInputModel model)
        {
            if (model != null)
            {
                var currentVote = this.Data.Votes.GetById(model.Id);
                var useCode = false;
                if (!currentVote.IsPublic)
                {
                    if (model.IdentificationCode != null)
                    {
                        var codeInDb = this.Data.IdentificatonCodes.GetById(model.IdentificationCode);
                        if (codeInDb.VoteId == null || codeInDb.VoteId != currentVote.Id)
                        {
                            this.TempData["Error"] = "Invalid Identification Code";
                        }

                        useCode = true;
                    }
                    else
                    {
                        this.TempData["Error"] = "Invalid Identification Code";
                    }
                }

                var checkedCandidates = model.Candidates.Where(x => x.IsChecked);
                if (checkedCandidates.Count() != currentVote.NumberOfVotes)
                {
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
                        }
                        else
                        {
                            this.TempData["Error"] = "Candidate does not match with the Vote";
                        }
                    }
                }

                if (useCode)
                {
                    var codeInDb = this.Data.IdentificatonCodes.GetById(model.IdentificationCode);
                    codeInDb.Used = true;
                    this.Data.IdentificatonCodes.Update(codeInDb);
                }

                this.Data.SaveChanges();
                this.TempData["Success"] = "Your vote is accepted";
                return this.RedirectToAction("All", "Votes");
            }

            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            return this.View();
        }

        private VoteWithCandidatesInputModel GetInputModelByVote(Vote vote, string code = null)
        {
            var viewModel = new VoteWithCandidatesInputModel()
            {
                Id = vote.Id,
                Title = vote.Title,
                NumberOfVotes = vote.NumberOfVotes,
                Candidates =
                    this.Data.Candidates.AllByVote(vote.Id)
                    .Project()
                    .To<CandidateInputModel>(),
                IdentificationCode = code
            };

            return viewModel;
        }
    }
}