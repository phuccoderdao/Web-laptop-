using LapOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapOnline.Controllers
{
    public class GioHangController : Controller
    {
        QLLaptopEntities1 db= new QLLaptopEntities1();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult ThemGioHang(int ms, string url)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sp = lstGiohang.Find(n => n.iMaSP == ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                lstGiohang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Laptop");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaSPKhoiGioHang(int iMaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sp != null) 
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Laptop");
                }
            }
            return RedirectToAction("GioHang");
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sp = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["SoLuong"].ToString());
               
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Laptop");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TenDN"] == null || Session["TenDN"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Laptop");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONHANG dh = new DONHANG();
            KHACHHANG kh = (KHACHHANG)Session["TenDN"];
            List<GioHang> lstGioHang = LayGioHang();
            dh.MaKH = kh.MaKH;
            dh.NgayDH = DateTime.Now;

            var NgayGiao = String.Format("{0:dd/MM/yyyy}", f["NgayGiao"]);
            dh.NgayGiaoHang = DateTime.Parse(NgayGiao);            
            dh.Gia = (decimal)TongTien();
            dh.TenNguoiNhan = kh.HoTenKH;
            dh.DiaChiNhan = kh.DiaChiKH;
            dh.DienThoaiNhan = kh.DienThoaiKH;
            dh.HTGiaoHang = true;
            dh.HTThanhToan = false;
            db.DONHANGs.Add(dh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                decimal ThanhTien = item.iSoLuong * (Decimal)item.dDonGia;
                CTDONHANG ctdh = new CTDONHANG();
                ctdh.SoHD = dh.SoHD;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                ctdh.ThanhTien = ThanhTien;
                db.CTDONHANGs.Add(ctdh);

            }
            Session["GioHang"] = null;
            db.SaveChanges();
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}