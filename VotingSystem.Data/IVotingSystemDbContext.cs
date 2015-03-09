namespace VotingSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using VotingSystem.Models;

    public interface IVotingSystemDbContext
    {

        IDbSet<Poll> Polls { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<Question> Question { get; set; }

        IDbSet<Answer> Answers { get; set; }

        IDbSet<Candidate> Candidates { get; set; }

        IDbSet<IdentificationCode> IdentificationCodes { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
