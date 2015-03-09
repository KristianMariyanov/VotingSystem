namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public interface IAnswersRepository : IGenericRepository<Answer>
    {

        IQueryable<Answer> AllByQuestion(int questionId);

        IQueryable<Answer> AllInQuestionByVotesCount(int questionId);
    }
}
