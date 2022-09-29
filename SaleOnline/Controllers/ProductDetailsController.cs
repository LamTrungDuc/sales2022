using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;

namespace SaleOnline.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetails
        public ActionResult Index(string MaSanPham)
        {
            //---- 1Dựa vào LINQ để lấy đối tượng sản phẩm từ data Model ------------
            ShopOnlineConnext db = new ShopOnlineConnext();
            SanPham x = db.SanPham.Where(sp => sp.maSP.Equals(MaSanPham)).First<SanPham>();
            //----2 làm sao để đưa vào view [đang ở controller]--------------
            ViewData["spCanXem"] = x;
            return View();

        }
    }
}