namespace VotingSystem.Web.ViewModels.Votes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;
    using VotingSystem.Web.ViewModels.Candidates;

    public class VoteWithCandidatesInputModel : IMapFrom<Vote>
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfVotes { get; set; }

        public IEnumerable<CandidateInputModel> Candidates { get; set; }
    }
}