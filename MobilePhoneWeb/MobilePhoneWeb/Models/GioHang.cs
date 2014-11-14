using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePhoneWeb.Models
{
    public class GioHang
    {
        //        QL_DienThoaiEntities db = new QL_DienThoaiEntities();

        ServiceDonHang.ServiceDonHangClient donhang = new ServiceDonHang.ServiceDonHangClient();
        ServiceDonHang.ServiceDonHangClient ctdh = new ServiceDonHang.ServiceDonHangClient();

        public List<CartLine> dssanpham = new List<CartLine>();
        public void AddItem(int Masp, string Tensp, int Soluong, double giatien, string hinh)
        {
            CartLine line = dssanpham
            .Where(p => p.masp == Masp).FirstOrDefault();

            if (line == null)
            {
                dssanpham.Add(new CartLine { masp = Masp, tensp = Tensp, soluong = Soluong, gia = giatien, hinhanh = hinh });
            }
            else
            {
                line.soluong += Soluong;

            }

        }

        public void AddChiTietHoaDon(ServiceDonHang.DonHang hd)
        {

            var line = dssanpham.ToList();

            foreach (var item in line)
            {
                var chitiethoadon = new ServiceDonHang.CT_DonHang
                {
                    MaSP = item.masp,
                    MaDH = hd.MaDH,
                    SoLuong = item.soluong,
                    Gia = (int)item.gia,
                    //                  Thanhtien = (int)item.soluong * (int)item.gia
                };
                //                donhang

                donhang.UpdateDonHang(hd);

                //                db.CT_DonHang.Add(chitiethoadon);
                ctdh.InsertCTDH(chitiethoadon);

                ctdh.UpdateCTDH(chitiethoadon);

            }


            //            db.SaveChanges();

        }

        public void Clear()
        {
            dssanpham.Clear();
        }
        public void RemoveLine(int masp)
        {
            dssanpham.RemoveAll(l => l.masp == masp);
        }

        public IEnumerable<CartLine> Lines
        {
            get { return dssanpham; }
        }

        public class CartLine
        {

            public int masp { get; set; }
            public string tensp { get; set; }
            public string hinhanh { get; set; }
            public double gia { get; set; }
            public int soluong { get; set; }
        }

        public void capnhatsoluong(int masp, int soluong)
        {
            CartLine line = dssanpham.Where(p => p.masp == masp).FirstOrDefault();
            line.soluong = soluong;
        }

        public double tongtien()
        {
            return dssanpham.Sum(e => e.gia * e.soluong);
        }

        public int sogiohang()
        {
            return dssanpham.Sum(e => e.soluong);
        }
    }
}