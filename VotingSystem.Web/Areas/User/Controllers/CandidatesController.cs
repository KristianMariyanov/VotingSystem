using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VotingSystem.Web.Areas.User.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VotingSystem.Data;
    using VotingSystem.Models;
    using VotingSystem.Web.Areas.User.ViewModels;

    public class CandidatesController : UserBaseController
    {
        // GET: User/Candidates
        public CandidatesController(IVotingSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All(int Id)
        {
            var candidates = this.Data
                .Candidates
                .AllByVote(Id)
                .Project()
                .To<CandidateViewModel>()
                .ToList();

            return this.View(candidates);
        }

        [HttpGet]
        public ActionResult Add(int Id)
        {
            var candidateModel = new CandidateViewModel() { VoteId = Id };
            return this.View(candidateModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CandidateViewModel candidate)
        {
            if (candidate != null && this.ModelState.IsValid)
            {
                candidate.VoteCount = 0;
                var candidateToDb = Mapper.DynamicMap<Candidate>(candidate);


                this.Data.Candidates.Add(candidateToDb);
                this.Data.SaveChanges();

                return this.RedirectToAction("All", new { id = candidate.VoteId });
            }

            return this.View(candidate);
        }
    }
}