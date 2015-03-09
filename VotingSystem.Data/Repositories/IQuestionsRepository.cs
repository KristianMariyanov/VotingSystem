namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public interface IQuestionsRepository : IGenericRepository<Question>
    {
        IQueryable<Question> AllAnswersByPoll(int pollId);
    }
}
