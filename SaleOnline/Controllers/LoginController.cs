using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;


namespace SaleOnline.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Acc, string Pass)
        {
            string mk = MaHoa.encryptSHA256(Pass);
            //-- Đọc thông tin tài khoản từ database thông qua Data model để xét có đúng tài khoản và mật khẩu không ----------------
            TaiKhoan ttdn = new ShopOnlineConnext()
                .TaiKhoan.Where(x =>x.taiKhoan1.Equals(Acc.ToLower().Trim()) && x.matKhau.Equals(mk))
                .First<TaiKhoan>();
            //--- nếu lấy được thông tin từ database và đúng ... thì cho phép các trang private 
            bool isAuthentic = ttdn !=null && ttdn.taiKhoan1.Equals(Acc.ToLower().Trim()) && ttdn.matKhau.Equals(mk);
            if (isAuthentic)
            {
                Session["TtDangNhap"] = ttdn;
                return RedirectToAction("Index", "Dashboard", new { Area = "PrivatePages" });
            }
                

              
            return View();
        }

    }
}