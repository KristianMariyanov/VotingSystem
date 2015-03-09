namespace VotingSystem.Web.Areas.User.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using VotingSystem.Data;
    using VotingSystem.Models;
    using VotingSystem.Web.Areas.User.ViewModels;

    using Model = VotingSystem.Models.Vote;
    using ViewModel = VotingSystem.Web.Areas.User.ViewModels.UserVotesViewModel;

    public class VotesController : KendoGridAdministrationController
    {
        // GET: User/Votes
        public VotesController(IVotingSystemData data)
            : base(data)
        {
        }

        public ActionResult Show()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Vote(int Id)
        {
            var voteWithCandidates = new VoteWithCandidatesInputModel()
                                 {
                                     Title = this.Data.Votes.TitleById(Id),
                                     Candidates =
                                         this.Data.Candidates.AllByVote(Id)
                                         .Project()
                                         .To<CandidateInputModel>()
                                 };
            return this.View(voteWithCandidates);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteWithCandidatesInputModel model)
        {
            if (model != null && this.ModelState.IsValid) { 
                foreach (var candidate in model.Candidates)
                {
                    if (candidate.IsChecked)
                    {
                        var currentCandidate = this.Data.Candidates.GetById(candidate.Id);
                        currentCandidate.VoteCount++;

                        this.Data.Candidates.Update(currentCandidate);
                        this.Data.SaveChanges();

                    }
                }

                this.RedirectToAction("All", new { id = model.Id });
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ViewModel model)
        {
            model.UserId = this.CurrentUser.Id;

            var modelToDb = base.Create<Vote>(model);
            if (modelToDb != null)
            {
                model.Id = modelToDb.Id;
                model.Author = modelToDb.User.UserName;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ViewModel model)
        {
            var firstOrDefault = this.Data.Users.All().FirstOrDefault(u => u.Votes.Any(p => p.Id == model.Id));
            if (firstOrDefault != null)
            {
                model.Author = firstOrDefault.UserName;
            }
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var poll = this.Data.Votes.GetById(model.Id);

                this.Data.Votes.Delete(poll);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            var data = this.Data.Votes.AllActive().Project().To<ViewModel>();

            return data;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Votes.GetById(id) as T;
        }
    }
}
