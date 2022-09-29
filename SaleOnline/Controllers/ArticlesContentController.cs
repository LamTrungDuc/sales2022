using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;

namespace SaleOnline.Controllers
{
    public class ArticlesContentController : Controller
    {
        // GET: ArticlesContent
        
        public ActionResult Index(string maBV)
        {
            ShopOnlineConnext db = new ShopOnlineConnext();
            BaiViet y = db.BaiViet.Where(n => n.maBV == maBV).First<BaiViet>();
            ViewData["BaiCanXems"] = y;
            return View();
        }
    }
}