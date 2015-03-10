namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public class CandidatesRepository : GenericRepository<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(IVotingSystemDbContext context)
            : base(context)
        {
        }

        public IQueryable<Candidate> AllByVote(int voteId)
        {
            return this.context.Candidates.Where(c => c.VoteId == voteId);
        }

        public IQueryable<Candidate> AllInVoteByVotesCount(int voteId)
        {
            return this.AllByVote(voteId).OrderBy(v => v.VoteCount);
        }
    }
}
