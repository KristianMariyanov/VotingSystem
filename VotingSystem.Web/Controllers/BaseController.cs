namespace VotingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using VotingSystem.Data;

    public class BaseController : Controller
    {
        public BaseController(IVotingSystemData data)
        {
            this.Data = data;
        }

        protected IVotingSystemData Data { get; set; }
    }
}