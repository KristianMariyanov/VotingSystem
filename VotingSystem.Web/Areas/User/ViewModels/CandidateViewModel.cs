namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class CandidateViewModel : IMapFrom<Candidate>
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int VoteCount { get; set; }


        [HiddenInput(DisplayValue = false)]
        public int VoteId { get; set; }

    }
}