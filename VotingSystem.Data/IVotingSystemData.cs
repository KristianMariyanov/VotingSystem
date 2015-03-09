namespace VotingSystem.Data
{
    using VotingSystem.Data.Repositories;
    using VotingSystem.Models;

    public interface IVotingSystemData
    {

        IVotingSystemDbContext Context{ get; }

        IGenericRepository<User> Users { get; }

        IVotesRepository Votes { get; }

        IPollRepository Polls { get; }

        IQuestionsRepository Questions { get; }

        IAnswersRepository Answers { get; }

        ICandidatesRepository Candidates { get; }

        IGenericRepository<IdentificationCode> IdentificatonCodes { get; }

        int SaveChanges();
    }
}
