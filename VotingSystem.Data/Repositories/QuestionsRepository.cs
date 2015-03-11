namespace VotingSystem.Data
{
    using System.Linq;

    using VotingSystem.Data.Repositories;
    using VotingSystem.Models;

    public class QuestionsRepository : GenericRepository<Question>, IQuestionsRepository
    {
        public QuestionsRepository(IVotingSystemDbContext context)
            : base(context)
        {
        }

        public IQueryable<Question> AllAnswersByPoll(int pollId)
        {
            return this.Context.Question.Where(q => q.PollId == pollId);
        }
    }
}
