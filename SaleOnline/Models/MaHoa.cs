using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace SaleOnline.Models
{
    public class MaHoa
    {
        /// <summary>
        /// Hàm phục vụ cho mục đích mã hóa một chuỗi văn bản gốc dựa vào việc băm dữ liệu bởi SHA256
        /// </summary>
        /// <param name="PlainText"></param>
        /// <returns>Kết quả đã mã hóa</returns>
        public static string encryptSHA256(string PlainText)
        {
            string result = "";
            //---- Create a SHA256 object ------------
            using (SHA256 bb = SHA256.Create())
            {
                //------ Convert PlanText to a bytes array
                byte[] sourceData = Encoding.UTF8.GetBytes(PlainText);
                //----Compute Hash and return a byte array -------------------
                byte[] hashResult = bb.ComputeHash(sourceData);
                result = BitConverter.ToString(hashResult);

            }
                return result;
        }
    }
}