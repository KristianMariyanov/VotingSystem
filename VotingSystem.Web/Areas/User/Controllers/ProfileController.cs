namespace VotingSystem.Web.Areas.User.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Web.Areas.User.ViewModels;

    public class ProfileController : UserBaseController
    {

        public ProfileController(IVotingSystemData data) : base(data)
        {        
        }
        // GET: User/Profile
        public ActionResult Show()
        {
            var userPollsAndVotes = new UserShowProfileViewModel()
                                        {
                                            ActivePolls =
                                                this.Data.Polls.AllActive()
                                                .OrderByDescending(x => x.StartDate)
                                                .Project()
                                                .To<UserPollsViewModel>()
                                                .ToList(),
                                            ActiveVotes =
                                                this.Data.Votes.AllActive()
                                                .OrderByDescending(x => x.StartDate)
                                                .Project()
                                                .To<UserVotesViewModel>()
                                                .Take(3)
                                                .ToList(),
                                        };
            return this.View(userPollsAndVotes);
        }
    }
}