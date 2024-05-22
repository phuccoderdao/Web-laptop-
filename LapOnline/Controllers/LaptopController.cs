using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapOnline.Models;
using PagedList;
using PagedList.Mvc;
namespace LapOnline.Controllers
{
    public class LaptopController : Controller
    {
        QLLaptopEntities1 db = new QLLaptopEntities1();
        // GET: Laptop
        private List<SANPHAM> LaySP(int count)
        {
            return db.SANPHAMs.Take(count).ToList();
        }

        public ActionResult Index()
        {
            var sp = LaySP(5);
            return View(sp);
        }

        // GET: Laptop/Details/5
        public ActionResult Details(int id)
        {
            var dt =from s in db.SANPHAMs where s.MaSP==id select s;
            return View(dt.Single());
        }
        public ActionResult Product(int page =1, int pagesize=4)
        {           
            var sp =LaySP(5).ToPagedList(page, pagesize);
            return View(sp);
        }
        public ActionResult LoaiSP()
        {
            
            return PartialView(db.LOAISPs.ToList());
        }

        // GET: Laptop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Laptop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Laptop/Edit/5
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

        // GET: Laptop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Laptop/Delete/5
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
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Laptop");
        }
    }
}
