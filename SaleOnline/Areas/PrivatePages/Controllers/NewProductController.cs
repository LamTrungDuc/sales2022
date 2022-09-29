using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SaleOnline.Areas.PrivatePages.Models;
using SaleOnline.Models;
using System.IO;

namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class NewProductController : Controller
    {
        // GET: PrivatePages/NewProduct Đăng sản phẩm mới 
        [HttpGet]
        public ActionResult Index()
        {
            SanPham x = new SanPham();
            x.ngayDang = DateTime.Now;
            x.maLoai = 7;           
            x.taiKhoan = ThuongDung.GetTenTaiKhoan();
            //-----Để đưa đg dẫn hình ra ngoài hiển thị trên images
            ViewBag.zzHinh = "/Asset/Images/sanPham/sp10.jpg";
            return View(x);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SanPham x,HttpPostedFileBase HinhDaiDien)
        {

            //--- B1xử lý thông tin nhận về từ view 
            x.maSP =string.Format("{0:yyMMddhhmm}", DateTime.Now);
            x.daDuyet = false;
            x.ngayDang = DateTime.Now;
            x.taiKhoan = ThuongDung.GetTenTaiKhoan();
            x.maLoai = 7;
            x.dvt = "Bit";
            //--------------------------
            if (HinhDaiDien != null)
            {
                // Lưu hình vào thư mục chứa sản phẩm
                string virPart = "/Asset/Images/sanPham/";
                string phyPath = Server.MapPath("~" + virPart); // xác định vị trí lưu hình sau khi upload 
                string ext = Path.GetExtension(HinhDaiDien.FileName);
                string tenF = "HDD" + x.maSP + ext;
                HinhDaiDien.SaveAs(phyPath + tenF); // lưu thì phải dựa vào đg dẫn vật lý [drive][path][fileName]
                // ghi nhận đg dẫn truy cập tới hình đã lưu trữ dựa vào domain
                x.hinhDD = virPart + tenF;    ///đường dẫn ảo theo domain
                //sẽ cập nhật hình vừa đăng cho giao diện 
                ViewBag.zzHinh = x.hinhDD;
            }
            else
                x.hinhDD = "";
           

            //x.hinhDD = "";
            //----B2 Cập nhật đối tượng bài viết vừa đăng vào datamodels
            ShopOnlineConnext db = new ShopOnlineConnext();
            db.SanPham.Add(x);
            //----B3 lưu thông tin xuống database
            db.SaveChanges();
            return View(x);
        }
    }
}