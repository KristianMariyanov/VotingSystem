namespace VotingSystem.Web.ViewModels.Home
{
    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class IndexVoteViewModel : IMapFrom<Vote>, IHaveCustomMappings
    {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Author { get; set; }

            public void CreateMappings(AutoMapper.IConfiguration configuration)
            {
                configuration.CreateMap<Vote, IndexVoteViewModel>()
                    .ForMember(m => m.Author, opt => opt.MapFrom(a => a.User.UserName));
            }
    }
}