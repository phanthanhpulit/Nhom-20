using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceKhachHang" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceKhachHang.svc or ServiceKhachHang.svc.cs at the Solution Explorer and start debugging.
    public class ServiceKhachHang : IServiceKhachHang
    {
        public List<KhachHang> SelectKhachHang()
        {
            var list = new List<KhachHang>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    //var khachhang = from p in db.KhachHangs select p;
                    var khachhang = from p in db.KhachHangs select new { p.MaKH, p.Username, p.Password, p.Email, p.HoTen, p.GioiTinh, p.NgaySinh, p.DiaChi, p.DienThoai };
                    foreach (var item in khachhang)
                    {
                        KhachHang kh = new KhachHang()
                        {
                            MaKH = item.MaKH,
                            Username = item.Username,
                            Password = item.Password,
                            Email = item.Email,
                            HoTen = item.HoTen,
                            GioiTinh = item.GioiTinh,
                            NgaySinh = item.NgaySinh,
                            DiaChi = item.DiaChi,
                            DienThoai = item.DienThoai
                        };
                        list.Add(kh);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertKhachHang(KhachHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    KhachHang inserted = new KhachHang
                    {
                        Username = info.Username,
                        Password = info.Password,
                        Email = info.Email,
                        HoTen = info.HoTen,
                        GioiTinh = info.GioiTinh,
                        NgaySinh = info.NgaySinh,
                        DiaChi = info.DiaChi,
                        DienThoai = info.DienThoai
                    };
                    db.Entry(inserted).State = EntityState.Added;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int UpdateKhachHang(KhachHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    KhachHang updated = new KhachHang
                    {
                        MaKH = info.MaKH,
                        Username = info.Username,
                        Password = info.Password,
                        Email = info.Email,
                        HoTen = info.HoTen,
                        GioiTinh = info.GioiTinh,
                        NgaySinh = info.NgaySinh,
                        DiaChi = info.DiaChi,
                        DienThoai = info.DienThoai
                    };
                    db.Entry(updated).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int DeleteKhachHang(KhachHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.KhachHangs
                                   where p.MaKH == info.MaKH
                                   select p).FirstOrDefault();
                    db.Entry(deleted).State = EntityState.Deleted;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        //Dang nhap
        public bool Login(string username, string password)
        {
            bool userValid;
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    userValid = db.KhachHangs.Any(user => user.Username == username && user.Password == password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    return false;
                }
            }
            return userValid;
        }

        //KIEM TRA USERNAME DANG KY
        public bool CheckUsername(string username)
        {
            bool userValid;
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    userValid = db.KhachHangs.Any(user => user.Username == username);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    return false;
                }
            }
            return userValid;
        }
    }
}
