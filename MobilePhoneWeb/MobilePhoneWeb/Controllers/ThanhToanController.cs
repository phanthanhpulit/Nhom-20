using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobilePhoneWeb.Models;

namespace MobilePhoneWeb.Controllers
{
    public class ThanhToanController : Controller
    {
        ServiceSanPham.ServiceSanPhamClient sanpham = new ServiceSanPham.ServiceSanPhamClient();
        ServiceDonHang.ServiceDonHangClient donhang = new ServiceDonHang.ServiceDonHangClient();
        ServiceDonHang.DonHang donh = new ServiceDonHang.DonHang();

        public ActionResult Index()
        {
            return View();
        }

        private GioHang GetCart()
        {
            GioHang cart = (GioHang)Session["Cart"];
            if (cart == null)
            {
                cart = new GioHang();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult DatHang(ServiceDonHang.DonHang hd)
        //ThongTinNguoiDatHang tthd)
        {

            //            DonHang hd = new DonHang();
            //            List<ServiceSanPham.SanPham> sp = db.SanPhams.ToList();
            List<ServiceSanPham.SanPham> sp = sanpham.SelectSanPham().ToList();
            //            var _hd = (from s in db.DonHangs orderby s.MaDH descending select s).FirstOrDefault();
            var _hd = (from s in donhang.SelectDonHang() orderby s.MaDH descending select s).FirstOrDefault();

            int i = _hd.MaDH;
            i++;

            var cart = GetCart();
            if (ModelState.IsValid)
            {
                if (Session["Account"] == null)
                {
                    /*
                    //                    var IDkh = db.KhachHangs.Single(u => u.MaKH == )
                    hd.MaDH = i;
                    //                    hd.MaKH = 
                    //                    hd.KhachHang.HoTen = tthd.tennguoinhan;
                    //                    hd.KhachHang.DienThoai = tthd.sodt;
                    //                    hd.KhachHang.DiaChi = tthd.diachi;
                    hd.Ngay = DateTime.Now;
                    hd.Trigia = (int)GetCart().tongtien();
                    //                    hd.KhachHang.Email = tthd.email;
                    hd.TinhTrang = 0;
                    donhang.InsertDonHang(hd);
                    //                    db.DonHangs.Add(hd);
                    donhang.UpdateDonHang(hd);

                    //                    db.SaveChanges();

                    //luu vao hoa don chi tiet
                    //                    GetCart().AddChiTietHoaDon(hd);
                    GetCart().AddChiTietHoaDon(hd);

                    foreach (var item in sp)
                    {
                        GetCart().RemoveLine(item.MaSP);
                    }
                    return RedirectToAction("HoanTat",
                             new { id = hd.MaDH });
                     * 
                     * */

                }
                else
                {
                    string cusID = Session["Account"].ToString();

                    hd.MaDH = i;
                    hd.Ngay = DateTime.Now;
                    hd.Trigia = (int)GetCart().tongtien();
                    hd.TinhTrang = 0;
                    hd.MaKH = 1;
                    donhang.InsertDonHang(hd);
                    donhang.UpdateDonHang(hd);


                    //var acc = db.KHACH_HANG.Single(u => u.KhachHangID == cusID);
                    //hd.HoaDonID = i;
                    //hd.KhachHangID = acc.KhachHangID;
                    //hd.TenNguoiNhan = tthd.tennguoinhan;
                    //hd.SoDienThoai = tthd.sodt;
                    //hd.DiaChiGiaoHang = tthd.diachi;
                    //hd.NgayDatHang = DateTime.Now;
                    //hd.ThanhTien = GetCart().tongtien();
                    //hd.Email = tthd.email;
                    //hd.TrangThai = "Chua lien lac";
                    //db.HOA_DON.Add(hd);
                    //db.SaveChanges();

                    //luu vao hoa don chi tiet
                    GetCart().AddChiTietHoaDon(hd);

                    foreach (var item in sp)
                    {
                        GetCart().RemoveLine(item.MaSP);
                    }
                    return RedirectToAction("HoanTat",
                             new { id = hd.MaDH });
                }

            }
            return View(hd);
        }

        public ActionResult HoanTat(int id)
        {
            //           bool isValid = db.DonHangs.Any(o => o.MaDH == id);
            //           bool isValid = donh

            //if (isValid)
            //{
            //    return View(id);
            //}
            //else
            //{
            //    return View("Error");
            //}
            return View(1);
        }

    }
}
