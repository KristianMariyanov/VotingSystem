

namespace VotingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Poll
    {
        private ICollection<Question> questions;

        private ICollection<IdentificationCode> identificationCodes;

        public Poll()
        {
            this.questions = new HashSet<Question>();
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

        public string UserId { get; set; }

        public virtual User User { get; set; }



        public virtual ICollection<IdentificationCode> IdentificationCodes
        {
            get { return this.identificationCodes; }
            set { this.identificationCodes = value; }
        }

        public virtual ICollection<Question> Questions 
        {
            get { return this.questions; }
            set { this.questions = value; }
        }
    }
}
