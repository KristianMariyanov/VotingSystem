namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class VoteWithCandidatesInputModel : IMapFrom<Vote>
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<CandidateInputModel> Candidates { get; set; }
    }
}