namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System.Collections.Generic;

    public class UserShowProfileViewModel
    {
        public UserShowProfileViewModel()
        {
            this.ActivePolls = new List<UserPollsViewModel>();
            this.ActiveVotes = new List<UserVotesViewModel>();
        }

        public IEnumerable<UserPollsViewModel> ActivePolls { get; set; }

        public IEnumerable<UserVotesViewModel> ActiveVotes { get; set; }
    }
}