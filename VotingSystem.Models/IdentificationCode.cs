namespace VotingSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class IdentificationCode
    {
        [Key]
        public int Id { get; set; }

        public int? PollId { get; set; }

        public virtual Poll Poll { get; set; }

        public int? VoteId { get; set; }

        public virtual Vote Vote { get; set; }

        [Required]
        public bool Used { get; set; }
    }
}
