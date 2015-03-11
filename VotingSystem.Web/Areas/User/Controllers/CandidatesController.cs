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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var candidateToDb = this.Data.Candidates.GetById(id);

            var candidate = Mapper.Map<CandidateViewModel>(candidateToDb);

            if (candidateToDb.Vote.UserId != this.CurrentUser.Id)
            {
                this.TempData["Error"] = "You can not change candidates this vote";
                return this.RedirectToAction("Show", "Votes");
            }

            return this.View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CandidateViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.Data.Candidates.DeleteById(model.Id);
                this.Data.SaveChanges();
                this.TempData["Success"] = "You successfully deleted a candiidate " + model.Name;
                return this.RedirectToAction("Moderate", "Candidates", new { id = model.VoteId });
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var candidateToDb = this.Data.Candidates.GetById(id);

            var candidate = Mapper.Map<CandidateViewModel>(candidateToDb);

            if (candidateToDb.Vote.UserId != this.CurrentUser.Id)
            {
                this.TempData["Error"] = "You can not change candidates this vote";
                return this.RedirectToAction("Show", "Votes");
            }

            return this.View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var candidateInDb = this.Data.Candidates.GetById(model.Id);
                if (candidateInDb != null)
                {
                    candidateInDb.Name = model.Name;
                    candidateInDb.Description = model.Description;
                }

                this.Data.Candidates.Update(candidateInDb);
                this.Data.SaveChanges();
                this.TempData["Success"] = "You successfully edited a candiidate " + model.Name;
                return this.RedirectToAction("Moderate", "Candidates", new { id = model.VoteId });
            }

            return this.View(model);
        }

        public ActionResult Moderate(int id)
        {
            var currentVote = this.Data.Votes.GetById(id);

            var voteInfo = new VoteWithCandidatesInputModel
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
            var candidateModel = new CandidateViewModel { VoteId = id };
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