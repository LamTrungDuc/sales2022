using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace SaleOnline.Models
{
    public class Common
    {
        static DbContext cn = new DbContext("ShopOnlineConnext");
        /// <summary>
        /// ham lay danh sach tat ca san pham thuoc mot loai nao do 
        /// </summary>
        /// <returns></returns>
        public static List<SanPham> getProduct()
        {
            List<SanPham> l = new List<SanPham>();
            //--- khai bao mot doi tuong dai dien cho database
            DbContext cn = new DbContext("name=ShopOnlineConnext");
            // lay du lieu ...
            l = cn.Set<SanPham>().ToList<SanPham>();
            return l;
        }

        /// <summary>
        /// hàm lấy ra danh sách tất cả các sản phẩm thuộc loại nào đó theo mã loại
        /// </summary>
        /// <param name="maLoai"></param>
        /// <returns></returns>
        public static List<SanPham> getProductByLoaiSP(int maLoai)
        {
            List<SanPham> l = new List<SanPham>();
            //--- khai bao mot doi tuong dai dien cho database
            DbContext cn = new DbContext("name=ShopOnlineConnext");
            // lay du lieu ...
            l = cn.Set<SanPham>().Where(x =>x.maLoai ==maLoai).OrderByDescending(z =>z.ngayDang).ToList<SanPham>();
            return l;
        }

        /// <summary>
        /// ham cho phep lay ra danh sach cac chung loai hang hoa
        /// </summary>
        /// <returns></returns>
        public static List<LoaiSP> getCategories()
        {
            return new DbContext("name=ShopOnlineConnext").Set<LoaiSP>().ToList<LoaiSP>();
        }
        /// <summary>
        /// ham cho phep lay ra danh sach cac Thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        public static List<KhachHang> getKhachHang()
        {
            return new DbContext("name=ShopOnlineConnext").Set<KhachHang>().ToList<KhachHang>();
        }
        /// <summary>
        /// ham cho phep lay ra danh sach cac Thông tin Đơn hàng
        /// </summary>
        /// <returns></returns>
        public static List<DonHang> getDonHang()
        {
            return new DbContext("name=ShopOnlineConnext").Set<DonHang>().ToList<DonHang>();
        }

        /// <summary>
        /// Lấy ra n bài viết mới nhất từ database
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<BaiViet> getArticles(int n)
        {
            List<BaiViet> l = new List<BaiViet>();
            ShopOnlineConnext db = new ShopOnlineConnext();
            l = db.BaiViet.Where(m =>m.daDuyet==true).OrderByDescending(bv => bv.ngayDang).Take(n).ToList<BaiViet>();
            return l;
        }

        public static LoaiSP GetLoaiSPById(int maLoai)
        {
            return cn.Set<LoaiSP>().Find(maLoai);
        }
        /// <summary>
        /// phương thức cho lấy thông tin của một sản phẩm dựa 
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>

            public static SanPham GetProductById(string maSP)
        {
            return cn.Set<SanPham>().Find(maSP);
        }
        /// <summary>
        /// Lấy tên của sản phẩm dựa vào mã 
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>
        public static string getNameOfProductByID(string maSP)
        {
            return cn.Set<SanPham>().Find(maSP).tenSP;
        }
        /// <summary>
        /// Lấy đường dẫn hình đại diện vào masp
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>
        public static string getImageOfProductByID(string maSP)
        {
            return cn.Set<SanPham>().Find(maSP).hinhDD;
        }
    }
}