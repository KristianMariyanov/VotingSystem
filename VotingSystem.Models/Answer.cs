namespace VotingSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string AnswerText { get; set; }

        public int VoteCount { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
