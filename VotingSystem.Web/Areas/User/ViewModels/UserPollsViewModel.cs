namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    using AutoMapper;

    public class UserPollsViewModel : IMapFrom<Poll>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        public bool IsPublic { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserId { get; set; }


        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Poll, UserPollsViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
        }
    }
}