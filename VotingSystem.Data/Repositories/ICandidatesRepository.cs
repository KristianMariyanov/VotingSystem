namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public interface ICandidatesRepository : IGenericRepository<Candidate>
    {
        IQueryable<Candidate> AllByVote(int voteId);

        IQueryable<Candidate> AllInVoteByVotesCount(int voteId);
    }
}
