namespace VotingSystem.Web.ViewModels.Home
{
    using System;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class IndexPollViewModel : IMapFrom<Poll>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }


        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Poll, IndexPollViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}