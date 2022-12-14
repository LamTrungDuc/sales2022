using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleOnline.Models
{
    public class CartShop
    {
        public string MaKH { get; set; }
        public string TaiKhoan { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public string DiaChi { get; set; }

        //?????
        //-----
        public SortedList<string, CtDonHang> SanPhamDC { get; set; }
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CartShop()
        {
            this.MaKH = ""; this.TaiKhoan = ""; this.DiaChi = "";
            this.NgayDat = DateTime.Now; this.NgayGiao = DateTime.Now.AddDays(2);
            this.SanPhamDC = new SortedList<string, CtDonHang>();
            
        }

        /// <summary>
        /// phương thức trả về là true nếu như không có sản phẩm nào đã chọn mua trong hệ thống
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return SanPhamDC.Keys.Count == 0;
        }
        /// <summary>
        /// phương thức thêm một sản phẩm đã chọn mua vào giỏ hàng 
        /// có 2 tình huống
        /// </summary>
        /// <param name="maSP"></param>
        /// 
       public void AddSanPham(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))
            {
                //Trỏ đến sản phẩm cần thay đổi số lượng mua có trong giỏ hàng 
                CtDonHang x = SanPhamDC.Values[SanPhamDC.IndexOfKey(maSP)];
                //-- tăng số lượng lên 1 
                x.soLuong++;
            }
            else
            {
                // tạo một object chi tiết đơn hàng 
                CtDonHang y = new CtDonHang();
                // cập nhật thông tin hiện hành từ hệ thống cho ddooiso tượng 
                y.maSP = maSP;
                y.soLuong = 1;
                // lấy giá bán ; lấy giảm giá từ table sản phẩm
                SanPham z = Common.GetProductById(maSP);
                y.giaBan = z.giaBan;
                y.giamGia = z.giamGia;
                //bỏ vào danh sách các sản phẩm đã chọn mua trong giỏ hàng của mình 
                SanPhamDC.Add(maSP, y);

            }
        }
    

        /// <summary>
        /// Xóa một sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="maSP"></param>

        public void deleteItem(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))          
                SanPhamDC.Remove(maSP);
        }
        /// <summary>
        /// cho phép giảm số lượng hoặc xóa sản phẩm đã chọn khỏi danh sách giỏ hàng
        /// </summary>
        /// <param name="maSP"></param>
        public void decrease(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))
            {
                //trỏ đến sản phẩm cần thay đổi số lượng mua trong giỏ hàng
                CtDonHang x = SanPhamDC.Values[SanPhamDC.IndexOfKey(maSP)];
                if (x.soLuong>1)
                
                    x.soLuong--;
               
                else
                    deleteItem(maSP);
            }
        }


        /// <summary>
        /// Tính trị giá tiền của 1 mặt hàng trong giỏ hàng
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        
        public long moneyOfOneProduct(CtDonHang x)
        {
            return (long) ((x.giaBan * x.soLuong) - (x.giaBan * x.soLuong * x.giamGia));
        }
        /// <summary>
        /// Tính tổng thành tiền cho toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public long totalOfCartShop()
        {
            long kq = 0;
            foreach (CtDonHang i in SanPhamDC.Values)
                kq += moneyOfOneProduct(i);
            return kq;
        }

    }
}