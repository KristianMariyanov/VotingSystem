namespace VotingSystem.Web.ViewModels.Votes
{
    using System;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class PublicActiveVotesViewModel : IMapFrom<Vote>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Author { get; set; }


        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Vote, PublicActiveVotesViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}