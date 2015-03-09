namespace VotingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class Candidate
    {
        [Key]
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
        public int VoteCount { get; set; }

        public string ImageURL { get; set; }

        public int VoteId { get; set; }

        public virtual Vote Vote { get; set; }
    }
}