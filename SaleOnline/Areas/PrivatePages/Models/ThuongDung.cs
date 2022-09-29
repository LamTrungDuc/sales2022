using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SaleOnline.Models;

namespace SaleOnline.Areas.PrivatePages.Models
{
    public class ThuongDung
    {
        /// <summary>
        /// Phương thức cho phép đọc thông tin của tài khoản đã đăng nhập ở trong session
        /// </summary>
        /// <returns></returns>
        public static TaiKhoan GetTaiKhoanHH()
        {
            TaiKhoan kq = new TaiKhoan();
            kq = HttpContext.Current.Session["TtDangNhap"] as TaiKhoan;
            return kq;
        }
        /// <summary>
        /// Lấy tên của tài khoản đã đăng nhập trong hệ thống 
        /// </summary>
        /// <returns></returns>
        public static string GetTenTaiKhoan()
        {
            return GetTaiKhoanHH().taiKhoan1;
        }
    }
}