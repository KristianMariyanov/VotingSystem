namespace VotingSystem.Web.Areas.User.Controllers
{
    using System;
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
        public VotesController(IVotingSystemData data)
            : base(data)
        {
        }

        public ActionResult Show()
        {
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

            if (!model.IsPublic)
            {
                for (int i = 0; i < model.Voters; i++)
                {
                    var code = new VoteIdentificationCodeViewModel()
                    {
                        VoteId = model.Id
                    };
                    var codeToDb = Mapper.DynamicMap<IdentificationCode>(code);

                    this.Data.IdentificatonCodes.Add(codeToDb);
                    this.Data.SaveChanges();
                }
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
            var data = this.Data.Votes.All().Where(x => x.UserId == this.CurrentUser.Id).Project().To<ViewModel>();

            return data;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Votes.GetById(id) as T;
        }
    }
}
