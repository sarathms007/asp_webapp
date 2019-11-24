using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP_webapp.wwwroot.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult FindSentiment(string Comment)
        {
            return Content(Comment);
        }
    }
    
}