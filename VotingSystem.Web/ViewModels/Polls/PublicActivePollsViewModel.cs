namespace VotingSystem.Web.ViewModels.Polls
{
    using System;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class PublicActivePollsViewModel : IMapFrom<Poll>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Author { get; set; }


        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Poll, PublicActivePollsViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}