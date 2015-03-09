namespace VotingSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.ActivePolls = new List<IndexPollViewModel>();
            this.ActiveVotes = new List<IndexVoteViewModel>();
        }

        public IEnumerable<IndexPollViewModel> ActivePolls { get; set; }

        public IEnumerable<IndexVoteViewModel> ActiveVotes { get; set; }
    }
}