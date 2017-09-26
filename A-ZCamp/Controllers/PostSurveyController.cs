using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A_ZCamp.Models;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class PostSurveyController : Controller
    {
        private ApplicationDbContext _dbContextPost;

        public PostSurveyController()
        {
            _dbContextPost = new ApplicationDbContext();
        }

        // GET: PostSurvey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(PostSurvey postsurvey)
        {
            _dbContextPost.PostSurvey.Add(postsurvey);
            _dbContextPost.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}