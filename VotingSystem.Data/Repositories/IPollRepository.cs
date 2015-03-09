namespace VotingSystem.Data.Repositories
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using VotingSystem.Models;

    public interface IPollRepository : IGenericRepository<Poll>
    {
        IQueryable<Poll> AllActive();

        IQueryable<Poll> AllByUser(string userId);
    }
}
