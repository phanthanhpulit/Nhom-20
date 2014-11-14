using MobilePhoneWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MobilePhoneWeb.Controllers
{
    public class UserController : Controller
    {
        ServiceKhachHang.ServiceKhachHangClient kh = new ServiceKhachHang.ServiceKhachHangClient();

        public ActionResult Index()
        {
            return View();
        }


        // CHỨC NĂNG ĐĂNG KÝ
        [HttpGet]
        public ActionResult FormRegister()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public ActionResult FormRegister(FormCollection f)
        //{
        //    int ngay = int.Parse(f["ngay"]);
        //    int thang = int.Parse(f["thang"]);
        //    int nam = int.Parse(f["nam"]);
        //    DateTime ns = new DateTime(nam, thang, ngay);

        //    ServiceKhachHang.KhachHang usr = new ServiceKhachHang.KhachHang();
        //    usr.Username = f["name"];
        //    usr.Password = f["pass"];
        //    usr.HoTen = f["name2"];
        //    usr.Email = f["email"];
        //    usr.DienThoai = f["phone"];
        //    usr.DiaChi = f["diachi"];
        //    //if (f["sex"] == "1") { usr.GioiTinh = true; } else usr.GioiTinh = false;
        //    usr.GioiTinh = f["gioitinh"];
        //    usr.NgaySinh = ns;

        //    if(kh.CheckUsername(usr.Username))
        //    {
        //        ViewBag.Mess = "Tên đăng nhập đã tồn tại!"; 
        //    }
        //    else{
        //        kh.InsertKhachHang(usr);
        //        ViewBag.Mess = "Đăng kí thành công!";
        //    }
        //    /*
        //    if (ModelState.IsValid)
        //    {
        //        string name = f["name"];
        //        bool userInValid = db.KhachHangs.Any(user => user.Username  == name);
        //        if (userInValid)
        //        {

        //            //ViewBag.Error = "<script language=javascript>alert('Đăng kí không thành công Ten đăng nhập bị trùng');</script>";
        //            ViewBag.Mess = "Tên đăng nhập đã tồn tại!";
        //        }
        //        else
        //        {
        //            //ViewBag.success = "<script language=javascript>alert('Đăng kí thành công!');</script>";
        //            db.KhachHangs.Add(usr);
        //            db.SaveChanges();
        //            return RedirectToAction("Index", "Index");

        //        }

        //    }
        //     */ 
        //    //ViewBag.ThongTinUsr = usr;
        //    return View(kh);
        //}

        public ActionResult FormRegister(FormCollection f, KhachHangModel khs)
        {
            ServiceKhachHang.KhachHang cus = new ServiceKhachHang.KhachHang();
            cus.Username = khs.UserName;
            cus.Password = khs.Password;
            cus.Email = khs.Email;
            cus.HoTen = khs.HoTen;
            cus.DiaChi = khs.DiaChi;
            cus.GioiTinh = khs.GioiTinh;
            cus.NgaySinh = khs.NgaySinh;
            if (ModelState.IsValid)
            {
                if (kh.CheckUsername(khs.UserName))
                {
                    ViewBag.Mess = 0;
                }
                else
                {
                    kh.InsertKhachHang(cus);
                    ViewBag.Mess = 1;
                }
            }
            return View(khs);
        }

        // CHỨC NĂNG ĐĂNG NHẬP
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string username = f["username"];
                string password = f["txtpass"];
                string _remember = f["remember"];
                bool remember;
                if (_remember == null)
                {
                    remember = false;
                }
                else
                {
                    remember = true;
                }

                bool userValid = kh.Login(username, password);
                // User tìm trong database
                if (userValid)
                {
                    Session["Account"] = username;
                    FormsAuthentication.SetAuthCookie(username, remember);

                    //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    //    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index", "Index");
                    //}

                    ViewBag.Mess = 1;
                }
                else
                {
                    //ViewBag.Error = "<script language=javascript>alert('Tên đăng nhập hoặc mật khẩu không đúng');</script>";
                    //ViewBag.Err = "<script language=javascript>alert('Sai thông tin đăng nhập!');</script>";
                    //return RedirectToAction("Index", "Index");
                    //ViewBag.Mess = 0;

                } 
                string url = this.Request.UrlReferrer.AbsolutePath;
                return Redirect(url);
            }
            return View(f);
        }



        // DANG XUAT
        public ActionResult LogOff()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            //return RedirectToAction("Index", "Index");
            string url = this.Request.UrlReferrer.AbsolutePath;

            return Redirect(url);
        }


        // Edit khách hàng
        public ActionResult EditUser()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Index");
            }
            else { 
                var user = kh.SelectKhachHang().Where(u => u.Username == Session["Account"].ToString());
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult EditUser(FormCollection f)
        {
            DateTime ns = DateTime.Parse(f["txtDate"].ToString());
            //string ns = f["txtDate"].ToString();
            ServiceKhachHang.KhachHang usr = new ServiceKhachHang.KhachHang();
            usr.Username = f["name"];
            usr.MaKH = kh.SelectKhachHang().Where(k => k.Username == usr.Username).First().MaKH;
            usr.Password = f["pass"];
            usr.HoTen = f["name2"];
            usr.Email = f["email"];
            usr.DienThoai = f["phone"];
            usr.DiaChi = f["diachi"];
            usr.GioiTinh = f["gioitinh"];
            usr.NgaySinh = ns;

            if (kh.UpdateKhachHang(usr)==1)
            {
                ViewBag.Mess = 1;
            }
            else
            {
                 ViewBag.Mess=null;
            }
            return View(kh.SelectKhachHang().ToList());
            //return RedirectToAction("Index", "Index");
        }
    }
}
