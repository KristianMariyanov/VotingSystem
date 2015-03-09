using System.ComponentModel.DataAnnotations;
namespace VotingSystem.Models
{
    using System.Collections.Generic;

    public class Question
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string QuestionText { get; set; }

        public int PollId { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual ICollection<Answer> Answers
        { 
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
