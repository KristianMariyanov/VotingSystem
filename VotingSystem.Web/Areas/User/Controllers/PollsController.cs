namespace VotingSystem.Web.Areas.User.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using VotingSystem.Data;

    using Model = VotingSystem.Models.Poll;
    using ViewModel = VotingSystem.Web.Areas.User.ViewModels.UserPollsViewModel;

    [Authorize]
    public class PollsController : KendoGridAdministrationController
    {
        public PollsController(IVotingSystemData data)
            : base(data)
        {
        }

        // GET: User/Polls
        public ActionResult Show()
        {
            return View();
        }





        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            model.UserId = this.CurrentUser.Id;

            var modelToDb = base.Create<Model>(model);
            if (modelToDb != null)
            {
                model.Id = modelToDb.Id;
                model.Author = modelToDb.User.UserName;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var firstOrDefault = this.Data.Users.All().FirstOrDefault(u => u.Polls.Any(p => p.Id == model.Id));
            if (firstOrDefault != null)
            {
                model.Author = firstOrDefault.UserName;
            }
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var poll = this.Data.Polls.GetById(model.Id);

                this.Data.Polls.Delete(poll);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            var data = this.Data.Polls
                           .AllActive()
                           .Project()
                           .To<ViewModel>();

            return data;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Polls.GetById(id) as T;
        }
    }
}