namespace VotingSystem.Data.Repositories
{
    using System;
    using System.Linq;

    using VotingSystem.Models;

    internal class PollsRepository : GenericRepository<Poll>, IPollRepository
    {
        public PollsRepository(IVotingSystemDbContext context)
            : base(context)
        {
        }

        public IQueryable<Poll> AllActive()
        {
            return this.Context.Polls.Where(p => p.EndDate > DateTime.Now);
        }

        public IQueryable<Poll> AllByUser(string userId)
        {
            return this.Context.Polls.Where(p => p.UserId == userId);
        }
    }
}
