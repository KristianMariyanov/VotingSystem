namespace VotingSystem.Data.Repositories
{
    using System.Linq;

    using VotingSystem.Models;

    public interface IVotesRepository : IGenericRepository<Vote>
    {
        string TitleById(int id);

        IQueryable<Vote> AllActive();

        IQueryable<Vote> AllByUser(string userId);
    }
}
