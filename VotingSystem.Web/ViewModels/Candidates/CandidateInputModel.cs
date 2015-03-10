namespace VotingSystem.Web.ViewModels.Candidates
{
    using VotingSystem.Web.Areas.User.ViewModels;

    public class CandidateInputModel : CandidateViewModel
    {
        public bool IsChecked { get; set; }
    }
}