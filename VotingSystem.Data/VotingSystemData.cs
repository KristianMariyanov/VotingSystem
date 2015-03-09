namespace VotingSystem.Data
{
    using System;
    using System.Collections.Generic;

    using VotingSystem.Data.Repositories;
    using VotingSystem.Models;

    public class VotingSystemData : IVotingSystemData
    {
        private IVotingSystemDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public VotingSystemData(IVotingSystemDbContext votingSystemDbContext)
        {
            this.context = votingSystemDbContext;
        }

        public IVotingSystemDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IPollRepository Polls
        {
            get
            {
                return (PollsRepository)this.GetRepository<Poll>();
            }
        }

        public IGenericRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IVotesRepository Votes
        {
            get
            {
                return (VotesRepository)this.GetRepository<Vote>();
            }
        }

        public IQuestionsRepository Questions
        {
            get
            {
                return (QuestionsRepository)this.GetRepository<Question>();
            }
        }

        public IAnswersRepository Answers
        {
            get
            {
                return (AnswersRepository)this.GetRepository<Answer>();
            }
        }

        public ICandidatesRepository Candidates
        {
            get
            {
                return (CandidatesRepository)this.GetRepository<Candidate>();
            }
        }

        public IGenericRepository<IdentificationCode> IdentificatonCodes
        {
            get
            {
                return this.GetRepository<IdentificationCode>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                if (typeof(T).IsAssignableFrom(typeof(Poll)))
                {
                    type = typeof(PollsRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(Vote)))
                {
                    type = typeof(VotesRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(Candidate)))
                {
                    type = typeof(CandidatesRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(Question)))
                {
                    type = typeof(QuestionsRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(Answer)))
                {
                    type = typeof(AnswersRepository);
                }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }
    }
}
