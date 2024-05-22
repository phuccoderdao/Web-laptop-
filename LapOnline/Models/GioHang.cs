using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapOnline.Models
{
    public class GioHang
    {
        QLLaptopEntities1 db = new QLLaptopEntities1();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }    
        public int iSoLuong { get; set; }
        public double dDonGia { get; set; }
        public double dThanhTien { get { return iSoLuong * dDonGia; } }
        
        public GioHang(int ms)
        {
            iMaSP = ms;
            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sHinhAnh=s.HinhAnh;
            iSoLuong = 1;
            dDonGia = double.Parse(s.DonGia.ToString());


        }
    }
}