using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MobilePhoneWeb.Models
{
    public partial class KhachHangModel
    {
        public int MaKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập UserName")]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "Không được sử dụng khoảng trắng")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessage = "Email không đúng định dạng.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string GioiTinh { get; set; }
        //public Nullable<System.DateTime> NgaySinh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập đúng định dạng: ngày/tháng/năm.")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
    }
}