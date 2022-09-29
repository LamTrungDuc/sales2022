using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;

namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class ArticlesController : Controller
    {
        private static ShopOnlineConnext db =new ShopOnlineConnext() ;
        private static bool daDuyet;
        // GET: PrivatePages/Articles
        [HttpGet]
        public ActionResult Index(string IsActive)
        {
           daDuyet =IsActive !=null && IsActive.Equals("1");
            CapNhatDuLieuCGD(daDuyet);
             return View();
        }
        [HttpPost]
        public ActionResult Delete(string maBaiViet)
        {
            //B1----Dùng lệnh để xóa baiviet dựa vào mã bài viết
            BaiViet x = db.BaiViet.Find(maBaiViet);
            db.BaiViet.Remove(x);

            //b2 cập nhật database
            db.SaveChanges();
            //b3 hiển thị lại danh sách sau khi xóa 
            CapNhatDuLieuCGD(daDuyet);
            return View("Index");
        }
        [HttpPost]
        public ActionResult Active(string maBaiViet)
        {
            //B1----Dùng lệnh để cấm baiviet dựa vào mã bài viết
            BaiViet x = db.BaiViet.Find(maBaiViet);
            x.daDuyet = !daDuyet;
            //b2 cập nhật database
            db.SaveChanges();
            //b3 hiển thị lại danh sách sau khi xóa 
            CapNhatDuLieuCGD(daDuyet);
            return View("Index");
        }
        /// <summary>
        /// Hàm phục vụ cho mục tiêu cập nhật dữ liệu cho view của controlles này thông qua Viewdata object
        /// </summary>
        private void CapNhatDuLieuCGD(bool isActive)
        {

            List<BaiViet> l = db.BaiViet.Where(x => x.daDuyet == isActive).ToList<BaiViet>();
            ViewData["DanhSachBV"] = l;
            ViewBag.tdCuaNut = daDuyet ? "Cấm hiển thị" : "Kiểm duyệt bài";
        }
    }
}