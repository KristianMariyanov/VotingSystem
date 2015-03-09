namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public interface IVotesRepository : IGenericRepository<Vote>
    {
        IQueryable<Vote> AllActive();

        IQueryable<Vote> AllByUser(string userId);
    }
}
