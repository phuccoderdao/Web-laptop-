using LapOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapOnline.Controllers
{
    public class UserController : Controller
    {
        QLLaptopEntities1 db = new QLLaptopEntities1();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HoTenKH"];
            var diachi = collection["DiaChiKH"];
            var dienthoai = collection["DienThoaiKH"];
            var ten = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var ngaysinh = String.Format("{0:MM/dd/yyy}", collection["NgaySinh"]);
            var gioitinh = collection["GioiTinh"];
            var email = collection["Email"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["er1"] = "Họ tên không được rỗng";

            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["er2"] = "Địa chỉ không được rỗng";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["er3"] = "Điện thoại không được rỗng";
            }
            else if (String.IsNullOrEmpty(ten))
            {
                ViewData["er4"] = "Tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["er5"] = "Bạn chưa đặt mật khẩu";
            }
            else if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["er6"] = "Bạn chưa chọn ngày sinh";
            }
            else if (String.IsNullOrEmpty(gioitinh))
            {
                ViewData["er7"] = "Bạn chưa chọn giới tính";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["er8"] = "Email không được rỗng";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["er9"] = "Địa chỉ không được rỗng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TenDN == ten) != null)
            {
                ViewBag.thongbao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == email) != null)
            {
                ViewBag.thongbao = "Email đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TenDN == ten) != null)
            {
                ViewBag.thongbao = "Tên đăng nhập đã tồn tại";
            }
            else
            {
                kh.HoTenKH = hoten;
                kh.DiaChiKH = diachi;
                kh.DienThoaiKH = dienthoai;
                kh.TenDN = ten;
                kh.MatKhau = matkhau;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.GioiTinh = bool.Parse(gioitinh);
                kh.Email = email;               
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "User");
            }
            return this.DangKy();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (string.IsNullOrEmpty(sTenDN))
            {
                ViewData["er1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["er2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    Session["TenDN"] = kh;

                    return RedirectToAction("Index", "Laptop");

                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}