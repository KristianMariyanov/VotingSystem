namespace VotingSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IdentificationCode
    {
        public IdentificationCode()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        [MaxLength(256)]
        public string Id { get; set; }

        public int? PollId { get; set; }

        public virtual Poll Poll { get; set; }

        public int? VoteId { get; set; }

        public virtual Vote Vote { get; set; }

        [Required]
        public bool Used { get; set; }
    }
}
