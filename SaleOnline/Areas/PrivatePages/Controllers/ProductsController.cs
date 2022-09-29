using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class ProductsController : Controller
    {
        // GET: PrivatePages/Products
        public ActionResult Index()
        {
            return View();
        }
    }
}