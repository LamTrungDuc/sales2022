using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleOnline.Controllers
{
    public class ArticlesListController : Controller
    {
        // GET: Articles    Danh sách bài viết 
        public ActionResult Index()
        {
            return View();
        }
    }
}