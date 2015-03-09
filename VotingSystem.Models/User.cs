namespace VotingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Poll> polls;
        private ICollection<Vote> votes;
        
        public User()
        {
            this.polls = new HashSet<Poll>();
            this.votes = new HashSet<Vote>();
        }

        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<Poll> Polls
        {
            get { return this.polls; }
            set { this.polls = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
