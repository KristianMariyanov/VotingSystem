namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public class AnswersRepository : GenericRepository<Answer>, IAnswersRepository
    {
        public AnswersRepository(IVotingSystemDbContext context)
            : base(context)
        {
        }

        public IQueryable<Answer> AllByQuestion(int questionId)
        {
            return this.Context.Answers.Where(c => c.QuestionId == questionId);
        }

        public IQueryable<Answer> AllInQuestionByVotesCount(int questionId)
        {
            return this.AllByQuestion(questionId).OrderBy(v => v.VoteCount);
        }
    }
}
