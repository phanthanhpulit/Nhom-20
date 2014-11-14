using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobilePhoneWeb.Models
{
    public class ThongTinNguoiDatHang
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        public string tennguoinhan { get; set; }

        [Required(ErrorMessage = "Email không được để trống !")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessage = "Email không đúng định dạng.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression("(09[0-9]{8}|01[2-9]{1}[0-9]{8})",
                   ErrorMessage = "số diên thoại không đúng định dạng.")]
        public string sodt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string diachi { get; set; }
    }
}