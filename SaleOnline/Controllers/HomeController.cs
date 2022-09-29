using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;


namespace SaleOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            string testMK = MaHoa.encryptSHA256("abc");
            List<SanPham> l = Common.getProductByLoaiSP(4);
            return View();
        }
       [HttpPost]
        public ActionResult AddTo(string maSP)
        {
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new CartShop();
            }
            // lấy giỏ hàng từ session ra 
            CartShop gh = (CartShop)Session["GioHang"];
            //CartShop gh = new CartShop();
            // thêm sản phẩm vừa chọn mua vào giỏ hàng 
            gh.AddSanPham(maSP);
            // cập nhật giỏ hàng vào trong session 
            Session["GioHang"] = gh;
            return View("Index");

        }
    }
}