namespace VotingSystem.Web.Areas.User.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using VotingSystem.Models;
    using VotingSystem.Web.Infrastructure.Mapping;

    public class VoteIdentificationCodeViewModel : IMapFrom<IdentificationCode>
    {
        public int VoteId { get; set; }
    }
}