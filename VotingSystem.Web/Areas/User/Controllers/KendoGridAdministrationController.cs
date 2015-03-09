namespace VotingSystem.Web.Areas.User.Controllers
{
    using System.Collections;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web.Mvc;

    using AutoMapper;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using VotingSystem.Data;

    public abstract class KendoGridAdministrationController : UserBaseController
    {
        public KendoGridAdministrationController(IVotingSystemData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult readData = this.GetData()
                                            .ToDataSourceResult(request);

            return this.Json(readData, JsonRequestBehavior.AllowGet);
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                T modelToDb = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(modelToDb, EntityState.Added);
                return modelToDb;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                TModel modelToDb = this.GetById<TModel>(id);
                Mapper.DynamicMap<TViewModel, TModel>(model, modelToDb);
                this.ChangeEntityStateAndSave(modelToDb, EntityState.Modified);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        private void ChangeEntityStateAndSave(object modelOfDb, EntityState state)
        {
            DbEntityEntry<object> entry = this.Data.Context.Entry(modelOfDb);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}