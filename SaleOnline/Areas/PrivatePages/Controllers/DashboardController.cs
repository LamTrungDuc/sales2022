using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;

namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class DashboardController : Controller
    {

        // GET: PrivatePages/Dashboard
        public ActionResult Index()
        {
            TaiKhoan x = (TaiKhoan)Session["TtDangNhap"];
            if (x == null)
            {

                Response.Redirect("~/Login");

            }
            return View();
        }
    }
}