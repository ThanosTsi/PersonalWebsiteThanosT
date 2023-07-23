using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.DataAccess.Repository.IRepository;
using PersonalWebsite.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace PersonalWebsite.Controllers
{
    public class LeagueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<TestModel> _testModelList;
        private readonly IList<CommentModel> _comments;

        public LeagueController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._testModelList = _unitOfWork.TestModel.GetAll().ToList();


            this._comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Id = 1,
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Id = 2,
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Id = 3,
                    Author = "Jordan Walke",
                    Text = "This is *another* comment"
                },
            };

        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("League/comments")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Comments()
        {
            //return Json(_testModelList);
            return Json(_comments);
        }

        [Route("League/comments/new")]
        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            // Create a fake ID for this comment
            comment.Id = _comments.Count + 1;
            _comments.Add(comment);
            return Content("Success :)");
        }
    }
}
