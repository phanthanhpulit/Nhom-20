using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobilePhoneWeb.Models;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using PagedList;

namespace MobilePhoneWeb.Controllers
{
    public class IndexController : Controller
    {
        ServiceSanPham.ServiceSanPhamClient sp = new ServiceSanPham.ServiceSanPhamClient();
        ServiceNhaSanXuat.ServiceNhaSanXuatClient nsx = new ServiceNhaSanXuat.ServiceNhaSanXuatClient();
        static int maloai;
//        QL_DienThoaiEntities db = new QL_DienThoaiEntities();
        public ActionResult Index()
        {
            //var pro = (from p in db.SanPhams
            //           select new SanPhams
            //           {
            //               MASP = p.MaSP,
            //               TENSP = p.TenSP,
            //               GIA = p.Gia,
            //                HINH = p.UrlHinh,
            //           }).OrderByDescending(x => x.MASP).Take(8);
            var pro = sp.SelectSanPham().OrderByDescending(x => x.MaSP).Take(8);
            ViewBag.Views = "Index";
            return View(pro);

        }

        public ActionResult sanPhamCaoCap()
        {
            //var pro1 = (from p in db.SanPhams
            //           select new SanPhams
            //           {
            //               MASP = p.MaSP,
            //               TENSP = p.TenSP,
            //               GIA = p.Gia,
            //               HINH = p.UrlHinh,
            //           }).OrderByDescending(x => x.GIA).Take(10);
            var pro1 = sp.SelectSanPham().OrderByDescending(x => x.Gia).Take(8);
            ViewBag.Views = "sanPhamCaoCap";
            return PartialView(pro1);
        }

        public ActionResult Details(int id)
        {
            //var pro = (from p in db.SanPhams
            //           where id == p.MaSP
            //           select new SanPhams
            //           {
            //               MASP = p.MaSP,
            //               TENSP = p.TenSP,
            //               GIA = p.Gia,
            //               HINH = p.UrlHinh,
            //               CHITIET = p.MoTa,                           
            //           });
            var pro = sp.DetailSanPham(id);
            maloai = pro.First().MaNSX;
            ViewBag.Views = "ChiTiet";
            return PartialView(pro);
        }

        //  HIỂN THỊ SẢN PHẨM THEO DANH MỤC

        public ActionResult LoaiSanPham(int id, int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var pro = sp.SelectSanPhamByCategory(id).OrderByDescending(x => x.Gia).ToPagedList(pageNum, pageSize);
            var ct = nsx.SelectNhaSanXuatById(pro.First().MaNSX).First();
            ViewBag.Views = "LoaiSanPham";
            ViewBag.Titles = ct.TenNSX;
            return PartialView(pro);
        }

        //BOX-Tim Kiem
        [ChildActionOnly]
        public ActionResult DSLoaiTimKiem()
        {
            var loaisp = nsx.SelectNhaSanXuat();
            return PartialView(loaisp);
        }


        //TIM KIEM NANG CAO
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult Timkiem(FormCollection f)
        {
            return RedirectToAction("TKnangcao", new { nhasanxuat = f["nhasanxuat"], giatu = f["giatu"], den = f["den"] });
        }
        public ActionResult TKnangcao(int nhasanxuat, string Str_giatu, string Str_den, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);

            nhasanxuat = int.Parse(Request.QueryString["nhasanxuat"]);
            Str_giatu = Request.QueryString["giatu"];
            Str_den = Request.QueryString["den"];
            
            
            var loaisp = nsx.SelectNhaSanXuatById(nhasanxuat);
            string loai = "";
            foreach (var item in loaisp){
                loai = item.TenNSX;
                //maloai = item.MaNSX;
            }
            ViewBag.KhongTimThay = "Không tìm thấy các sản phẩm thỏa điều kiện!";
            if (nhasanxuat == 0)
                ViewBag.Loaihang = "Tất cả mặt hàng";
            else ViewBag.Loaihang = loai;
            if (!IsNumber(Str_giatu))
                ViewBag.Giatu = "0";
            else ViewBag.Giatu = Str_giatu;
            if (!IsNumber(Str_den))
                ViewBag.Giaden = "Không giới hạn";
            else ViewBag.Giaden = Str_den;

            var pro = sp.SearchSanPham(nhasanxuat, Str_giatu, Str_den).OrderByDescending(x => x.Gia).ToPagedList(pageNum, pageSize);

            ViewBag.nhasanxuat = nhasanxuat;
            //ViewBag. url = this.Request.UrlReferrer.AbsolutePath;
            return View(pro);
        }

        public ActionResult HienThiMenuSP()
        {
            var mn = nsx.SelectNhaSanXuat().ToList();
            ViewBag.Views = "HienThiMenuSP";
            return PartialView(mn);
        }

        public ActionResult SanPhamCungLoai()
        {
            var pro1 = sp.SelectSanPham().Where(p => p.MaNSX == maloai).Take(8);
            ViewBag.Views = "SanPhamCungLoai";
            return PartialView(pro1);
        }
    }
}
