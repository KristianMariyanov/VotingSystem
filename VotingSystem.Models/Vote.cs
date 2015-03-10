namespace VotingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        private ICollection<Candidate> candidates;

        private ICollection<IdentificationCode> identificationCodes;

        public Vote()
        {
            this.candidates = new HashSet<Candidate>();
            this.identificationCodes = new HashSet<IdentificationCode>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int NumberOfVotes { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<IdentificationCode> IdentificationCodes
        {
            get { return this.identificationCodes; }
            set { this.identificationCodes = value; }
        }

        public virtual ICollection<Candidate> Candidates 
        {
            get { return this.candidates; }
            set { this.candidates = value; }
        }
    }
}
