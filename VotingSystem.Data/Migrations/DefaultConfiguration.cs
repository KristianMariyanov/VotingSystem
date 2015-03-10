namespace VotingSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VotingSystem.Models;

    public sealed class DefaultConfiguration : DbMigrationsConfiguration<VotingSystemDbContext>
    {
        private readonly Random random = new Random(0);

        public DefaultConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VotingSystemDbContext context)
        {
            if (context.Users.Any())
            {
                // Seed initial data only if the database is empty
                return;
            }

            var users = this.SeedApplicationUsers(context);
            var polls = this.SeedPolls(context, users);
            var votes = this.SeedVotes(context, users);
            var questions = this.SeedQuestions(context, polls);
            this.SeedAnswers(context, questions);
            this.SeedCandidates(context, votes);
        }

        private void SeedCandidates(VotingSystemDbContext context, IList<Vote> votes)
        {
            foreach (var vote in votes)
            {
                var candidates = new List<Candidate>()
                                     {
                                         new Candidate()
                                             {
                                                 Name = "Pesho",
                                                 Description = "Vote for me",
                                                 VoteCount = 0,
                                                 Vote = vote
                                             },
                                         new Candidate()
                                             {
                                                 Name = "Gosho Goshov",
                                                 Description = "I'm the best",
                                                 VoteCount = 0,
                                                 Vote = vote
                                             },
                                         new Candidate()
                                             {
                                                 Name = "Boiko Borisov",
                                                 Description = "I'll buy you meatballs",
                                                 VoteCount = 0,
                                                 Vote = vote
                                             },
                                         new Candidate()
                                             {
                                                 Name = "Georgi Purvanov",
                                                 Description = "I'll set things right",
                                                 VoteCount = 0,
                                                 Vote = vote
                                             }
                                     };
                foreach (var candidate in candidates)
                {
                    context.Candidates.AddOrUpdate(candidate);
                }

                context.SaveChanges();
            }
        }

        private void SeedAnswers(VotingSystemDbContext context, IList<Question> questions)
        {
            foreach (var question in questions)
            {
                var answers = new List<Answer>()
                                  {
                                      new Answer()
                                          {
                                              AnswerText = "Yes",
                                              VoteCount = 0,
                                              Question = question
                                          },
                                      new Answer()
                                          {
                                              AnswerText = "No!",
                                              VoteCount = 0,
                                              Question = question
                                          },
                                      new Answer()
                                          {
                                              AnswerText = "Maybe",
                                              VoteCount = 0,
                                              Question = question
                                          }
                                  };
                foreach (var answer in answers)
                {
                    context.Answers.AddOrUpdate(answer);
                }

                context.SaveChanges();
            }
        }

        private IList<Question> SeedQuestions(VotingSystemDbContext context, IList<Poll> polls)
        {
            var questions = new List<Question>
                            {
                                new Question()
                                    {
                                        QuestionText = "Do you like IceCream",
                                        Poll = polls[this.random.Next(polls.Count)]
                                    },
                                    new Question()
                                    {
                                        QuestionText = "Do you like Linux",
                                        Poll = polls[this.random.Next(polls.Count)]
                                    },
                                    new Question()
                                    {
                                        QuestionText = "Do you like ASP.NET MVC",
                                        Poll = polls[this.random.Next(polls.Count)]
                                    },
                                    new Question()
                                    {
                                        QuestionText = "Do you like SoftUni",
                                        Poll = polls[this.random.Next(polls.Count)]
                                    }
                            };

            foreach (var question in questions)
            {
                context.Question.AddOrUpdate(question);
            }

            context.SaveChanges();

            return questions;
        }

        private IList<Vote> SeedVotes(VotingSystemDbContext context, IList<User> users)
        {
            var votes = new List<Vote>
                            {
                                new Vote()
                                    {
                                        Title = "Vote for Students Council",
                                        IsPublic = true,
                                        StartDate = DateTime.Now,
                                        EndDate = DateTime.Now.AddDays(10),
                                        NumberOfVotes = 3,
                                        User = users[this.random.Next(users.Count)]
                                    },
                                    new Vote()
                                    {
                                        Title = "Vote for government",
                                        IsPublic = true,
                                        StartDate = DateTime.Now,
                                        EndDate = DateTime.Now.AddDays(10),
                                        NumberOfVotes = 2,
                                        User = users[this.random.Next(users.Count)]
                                    },
                                    new Vote()
                                    {
                                        Title = "Vote for President",
                                        IsPublic = true,
                                        StartDate = DateTime.Now,
                                        EndDate = DateTime.Now.AddDays(10),
                                        NumberOfVotes = 1,
                                        User = users[this.random.Next(users.Count)]
                                    }
                            };

            foreach (var vote in votes)
            {
                context.Votes.AddOrUpdate(vote);
            }

            context.SaveChanges();

            return votes;
        }

        private IList<User> SeedApplicationUsers(
            VotingSystemDbContext context)
        {
            var usernames = new string[] { "kiko", "maria", "peter", "kiro", "didi" };

            var users = new List<User>();
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            foreach (var username in usernames)
            {
                var name = username[0].ToString().ToUpper() + username.Substring(1);
                var user = new User { UserName = username, Name = name, Email = username + "@gmail.com" }; 

                var password = username;
                var userCreateResult = userManager.Create(user, password);
                if (userCreateResult.Succeeded)
                {
                    users.Add(user);
                }
                else
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }
            }

            context.SaveChanges();

            return users;
        }

        private IList<Poll> SeedPolls(
           VotingSystemDbContext context, IList<User> users)
        {
            var polls = new List<Poll>
                            {
                                new Poll()
                                    {
                                        Title = "What is your favourite things",
                                        IsPublic = true,
                                        StartDate = DateTime.Now,
                                        EndDate = DateTime.Now.AddDays(10),
                                        User = users[this.random.Next(users.Count)]
                                    },
                                    new Poll()
                                    {
                                        Title = "Tipical Poll",
                                        IsPublic = true,
                                        StartDate = DateTime.Now,
                                        EndDate = DateTime.Now.AddDays(10),
                                        User = users[this.random.Next(users.Count)]
                                    }
                            };

            foreach (var poll in polls)
            {
                context.Polls.AddOrUpdate(poll);
            }

            context.SaveChanges();

            return polls;
        }
    }
}
