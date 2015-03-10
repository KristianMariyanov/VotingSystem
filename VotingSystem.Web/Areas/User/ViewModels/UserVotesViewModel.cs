namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System;
    using System.Web.Mvc;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class UserVotesViewModel : IMapFrom<Vote>, IHaveCustomMappings
    {

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        public int NumberOfVotes { get; set; }

        public bool IsPublic { get; set; }

        public int Voters { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }


        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Vote, UserVotesViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}