using LapOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace LapOnline.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        
        // GET: Admin/SanPham
        QLLaptopEntities1 db = new QLLaptopEntities1();
        public ActionResult Index(int? page)
        {
            int ipageNum = (page ?? 1);
            int ipageSize = 6;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(ipageNum, ipageSize));
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/SanPham/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SANPHAMs.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp ,FormCollection collection, HttpPostedFileBase fFileUpLoad)
        {
            if (fFileUpLoad == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa";
                ViewBag.TenSP = collection["sTenSP"];
                ViewBag.TenNSX = collection["sTenNSX"];
                ViewBag.RAM = collection["iRAM"];
                ViewBag.ViXuLy = collection["sViXuLy"];
                ViewBag.DonGia = decimal.Parse(collection["mDonGia"]);
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName=Path.GetFileName(fFileUpLoad.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpLoad.SaveAs(path);
                    }
                    sp.TenSP = collection["sTenSP"];
                   
                    sp.HinhAnh = sFileName;
                    sp.RAM = int.Parse(collection["iRAM"]);
                    sp.ViXuLy = collection["sViXuLy"];
                    sp.NgaySX = Convert.ToDateTime(collection["dNgaySX"]);
                    sp.DonGia = decimal.Parse(collection["mDonGia"]);
                    //db.SANPHAMs.InsertOnSubmit(sp);
                    //db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
