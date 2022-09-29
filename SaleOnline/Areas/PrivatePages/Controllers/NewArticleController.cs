using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SaleOnline.Models;
using SaleOnline.Areas.PrivatePages.Models;
using System.IO;
namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class NewArticleController : Controller
    {
        // GET: PrivatePages/NewArticle Đăng bài viết mới 
        [HttpGet]
        public ActionResult Index()
        {
            BaiViet x = new BaiViet();
            //--- thiết lập một số thông tin mặc định cần gán cho đối tượng bài viết khách quan nhất
            x.ngayDang = DateTime.Now;
            x.soLanDoc = 0; 
            x.taiKhoan = ThuongDung.GetTenTaiKhoan();
            //-------------Để đưa đg dẫn hình ra ngoài hiển thị trên images
            ViewBag.ddHinh = "/Asset/Images/sanPham/sp11.jpg";
            return View(x);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(BaiViet x ,HttpPostedFileBase HinhDaiDien)
        {

            try
            {
                //----B1 xử lý thông tin nhận về từ view
                x.maBV = string.Format("{0:yyMMddhhmm}", DateTime.Now);
                x.daDuyet = false;
                x.ngayDang = DateTime.Now;
                x.taiKhoan = ThuongDung.GetTenTaiKhoan();
                x.soLanDoc = 0;
                x.loaiTin = "QC";
                //x.hinhDD------------------------
                // sẽ cập nhật hình vừa đăng cho giao diện 
                ViewBag.ddHinh = x.hinhDD;

                //--------------------------------
                if (HinhDaiDien != null)
                {
                    //---- Lưu hình vào thư mục chưa bài viết
                    string virPath = "/Asset/Images/sanPham/";
                    string phyPath = Server.MapPath("~/" + virPath); // xác định vị trí lưu hình sau khi upload
                    string ext = Path.GetExtension(HinhDaiDien.FileName);
                    string tenF = "HDD" + x.maBV + ext;
                    HinhDaiDien.SaveAs(phyPath + tenF);   // lưu thì phải dựa vào đg dẫn vật lý [drive][path][filename]
                                                          //ghi nhận đường dẫn truy cập tới hình đã lưu trữ dựa vào domain
                    x.hinhDD = virPath + tenF;   // cập nhật đg dẫn ảo theo domain

                    ViewBag.ddHinh = x.hinhDD;
                }
                else
                    x.hinhDD = "";




                //---B2 Cập Nhật đối tượng bài viết vừa đăng vào datamodel
                ShopOnlineConnext db = new ShopOnlineConnext();
                db.BaiViet.Add(x);

                //---B3 lưu thông tin xuống database
                db.SaveChanges();
                // nếu thành công chuyển đến trang danh sách các sản phẩm chưa được hiển thị
                return RedirectToAction("Index", "Articles", new { IsActive = 0 });
            }
            catch
            {

            }

            return View(x);
        }

    }
}