using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using SaleOnline.Models;

namespace SaleOnline.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        [HttpGet]
        public ActionResult Index()
        {
            // tạo một đối tượng khách hàng với thông tin mới truyền ra cho view 
            KhachHang x = new KhachHang();
            // lấy thông tin đã mua hàng từ session và truyền cho view thông qua viewData
          
            //lấy giỏ hàng từ sesstion
            CartShop gh = Session["GioHang"] as CartShop;
            //truyền ra ngoài view
            ViewData["Cart"] = gh;
            return View(x);
        }
        [HttpPost]
        public ActionResult SaveToDaTaBase(KhachHang x)
        {
          
            // ứng dụng transaction để lưu đồng thời 3 table khách nhau

            using (var context = new ShopOnlineConnext())
            {

                using (DbContextTransaction trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        // 1.1/ new a customer object and add to KhachHang domain [table KhachHang]-----
                        // 1.2 Update customer KhachHang
                        x.maKH = x.soDT;
                        //1.3 Add customer infor data model
                        context.KhachHang.Add(x);
                        // 1.4 save to database
                        context.SaveChanges();
                        // 2.1/ new an Order object and add to KhachHang domain [table DonHang]-----
                        DonHang d = new DonHang();

                        // 2.2 Update customer DonHang
                        d.maKH = x.maKH;
                        d.soDH = string.Format("0:yyMMddhhmm", DateTime.Now);
                        d.ngayDat = DateTime.Now;
                        d.ngayGH = DateTime.Now.AddDays(2);
                        d.taiKhoan = "admin";
                        d.diaChiGH = x.diaChi;
                        //2.3 Add customer infor data model
                        context.DonHang.Add(d);
                        // 2.4 save to database DonHang
                        context.SaveChanges();
                        // 3.1/ Get list of Items from CartShop [table CtDonHang]-----
                        CartShop gh = Session["GioHang"] as CartShop;
                        // 3.2 Update customer CtDonHang
                        foreach (CtDonHang i in gh.SanPhamDC.Values)
                        {
                            i.soDH = d.soDH;
                            context.CtDonHang.Add(i);
                        }
                        //3.3 Add customer infor data model

                        // 3.4 save to database CtDonHang
                        context.SaveChanges();
                        // 4 finish and commits all of action above
                        trans.Commit();
                        //chuyển đến trang thông báo đã đặt hàng thành công [chuyển về home pages]
                        return RedirectToAction("Index", "Home");
                    }
                    catch(Exception e)
                    {
                        trans.Rollback();
                        string s = e.Message;
                    }
                }
            }
            
                // nếu bị lỗi sẽ chuyển về checkOut
                return RedirectToAction("Index", "CheckOut");
        }
    }
}