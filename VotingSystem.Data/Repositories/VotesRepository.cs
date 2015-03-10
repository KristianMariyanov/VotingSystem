namespace VotingSystem.Data
{
    using System;
    using System.Linq;

    using VotingSystem.Data.Repositories;
    using VotingSystem.Models;

    public class VotesRepository : GenericRepository<Vote>, IVotesRepository 
    {
        public VotesRepository(IVotingSystemDbContext context)
            : base(context)
        {
        }

        public string TitleById(int id)
        {
            return this.context.Votes.Where(i => i.Id == id).Select(t => t.Title).FirstOrDefault();
        }

        public IQueryable<Vote> AllActive()
        {
            return this.context.Votes.Where(v => v.EndDate > DateTime.Now && v.IsPublic);
        }

        public IQueryable<Vote> AllByUser(string userId)
        {
            return this.context.Votes.Where(v => v.UserId == userId);
        }
    }
}
