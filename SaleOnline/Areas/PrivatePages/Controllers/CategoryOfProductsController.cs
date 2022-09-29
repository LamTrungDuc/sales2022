using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleOnline.Models;

namespace SaleOnline.Areas.PrivatePages.Controllers
{
    public class CategoryOfProductsController : Controller
    {
        private static bool isUpdate = false;
        // GET: PrivatePages/CategoryOfProducts
        [HttpGet]
        public ActionResult Index()
        {
            List<LoaiSP> l = new ShopOnlineConnext().LoaiSP.OrderBy(x => x.tenLoai).ToList<LoaiSP>();
            ViewData["DsLoai"] =l;
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoaiSP x)
        {
            ShopOnlineConnext db = new ShopOnlineConnext();
            //--- B1 LoaiSP object to Data model
            if (!isUpdate)
                db.LoaiSP.Add(x);
            else
            {
                LoaiSP y = db.LoaiSP.Find(x.maLoai);
                y.tenLoai = x.tenLoai;
                y.ghiChu = x.ghiChu;
                isUpdate = false;
            }
            db.LoaiSP.Add(x);
            db.SaveChanges(); // save data to database
            //---- Update list to view  
            if (ModelState.IsValid)
                ModelState.Clear();
            ViewData["DsLoai"] = db.LoaiSP.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string ml)
        {
            ShopOnlineConnext db = new ShopOnlineConnext();
            int ma = int.Parse(ml);
            //find LoaiSP object in Data model by maLoai------------
            
            LoaiSP x = db.LoaiSP.Find(ma);
            //B2 Remove LoaiSP object form data models ---------
            db.LoaiSP.Remove(x);
            //-----B3 :Update to Database
            db.SaveChanges();
            //----Update Data on View
            ViewData["DsLoai"] = db.LoaiSP.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View("Index");
        }
        public ActionResult Update(string mlcs)
        {
            ShopOnlineConnext dba = new ShopOnlineConnext();
            int ma = int.Parse(mlcs);
            //find LoaiSP object in Data model by maLoai------------
            LoaiSP x = dba.LoaiSP.Find(ma);
            isUpdate = true;
            //--------
            ViewData["DsLoai"] = dba.LoaiSP.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View("Index",x);
        }
    }
}