using LapOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapOnline.Areas.Admin.Controllers
{
    public class homeController : Controller
    {
        QLLaptopEntities1 db=new QLLaptopEntities1();   
        // GET: Admin/home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult LoginAdmin(FormCollection f)
        {

            var sTenDN = f["userName"];
            var sMatKhau = f["password"];
            User ad = db.Users.SingleOrDefault(n => n.TenDNAdmin == sTenDN && n.MatKhauAdmin == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "home");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
       
        
    }
}