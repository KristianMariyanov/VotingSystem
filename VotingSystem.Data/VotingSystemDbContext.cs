namespace VotingSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.AspNet.Identity.EntityFramework;
    using VotingSystem.Data.Migrations;

    using VotingSystem.Models;

    public class VotingSystemDbContext : IdentityDbContext<User>, IVotingSystemDbContext
    {

        public VotingSystemDbContext()
            : base("VotingSystem", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VotingSystemDbContext, 
            DefaultConfiguration>()); 
        }

        public IDbSet<Poll> Polls { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<Question> Question { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Candidate> Candidates { get; set; }

        public IDbSet<IdentificationCode> IdentificationCodes { get; set; }
        
        public static VotingSystemDbContext Create()
        {
            return new VotingSystemDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        foreach (var exeption in ex.Entries)
        //        {
        //            throw;
        //        }
        //    }
        //    return 1;
        //}
    }
}
