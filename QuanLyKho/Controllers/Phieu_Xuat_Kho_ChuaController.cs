using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models.ModelDB;
using QuanLyKho.Models.ViewModel;
using QuanLyKho.Models.ModelManager;
using PagedList;

namespace QuanLyKho.Controllers
{
    [Authorize]
    public class Phieu_Xuat_Kho_ChuaController : Controller
    {
        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Phieu_Xuat_Kho_Chua
        public ActionResult Index()
        {
            
            return View(db.Kho_Chua.Where(x=>x.So_Luong>0).ToList());
        }

        // GET: Phieu_Xuat_Kho_Chua/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua = db.Phieu_Xuat_Kho_Chua.Find(id);
            if (phieu_Xuat_Kho_Chua == null)
            {
                return HttpNotFound();
            }
            return View(phieu_Xuat_Kho_Chua);
        }

        // GET: Phieu_Xuat_Kho_Chua/Create
        public ActionResult Create()
        {
            ViewBag.Hang_Hoa_Id = new SelectList(db.Kho_Chua, "Hang_Hoa_Id", "Hang_Hoa_Id");
            ViewBag.Phieu_Xuat_Id = new SelectList(db.Phieu_Xuat, "Phieu_Xuat_Id", "Phieu_Xuat_Id");
            return View();
        }

        // POST: Phieu_Xuat_Kho_Chua/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hang_Hoa_Id,Phieu_Xuat_Id,Don_Gia,So_Luong")] Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua)
        {
            if (ModelState.IsValid)
            {
                db.Phieu_Xuat_Kho_Chua.Add(phieu_Xuat_Kho_Chua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Hang_Hoa_Id = new SelectList(db.Kho_Chua, "Hang_Hoa_Id", "Hang_Hoa_Id", phieu_Xuat_Kho_Chua.Hang_Hoa_Id);
            ViewBag.Phieu_Xuat_Id = new SelectList(db.Phieu_Xuat, "Phieu_Xuat_Id", "Phieu_Xuat_Id", phieu_Xuat_Kho_Chua.Phieu_Xuat_Id);
            return View(phieu_Xuat_Kho_Chua);
        }

        // GET: Phieu_Xuat_Kho_Chua/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua = db.Phieu_Xuat_Kho_Chua.Find(id);
            if (phieu_Xuat_Kho_Chua == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hang_Hoa_Id = new SelectList(db.Kho_Chua, "Hang_Hoa_Id", "Hang_Hoa_Id", phieu_Xuat_Kho_Chua.Hang_Hoa_Id);
            ViewBag.Phieu_Xuat_Id = new SelectList(db.Phieu_Xuat, "Phieu_Xuat_Id", "Phieu_Xuat_Id", phieu_Xuat_Kho_Chua.Phieu_Xuat_Id);
            return View(phieu_Xuat_Kho_Chua);
        }

        // POST: Phieu_Xuat_Kho_Chua/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hang_Hoa_Id,Phieu_Xuat_Id,Don_Gia,So_Luong")] Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieu_Xuat_Kho_Chua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hang_Hoa_Id = new SelectList(db.Kho_Chua, "Hang_Hoa_Id", "Hang_Hoa_Id", phieu_Xuat_Kho_Chua.Hang_Hoa_Id);
            ViewBag.Phieu_Xuat_Id = new SelectList(db.Phieu_Xuat, "Phieu_Xuat_Id", "Phieu_Xuat_Id", phieu_Xuat_Kho_Chua.Phieu_Xuat_Id);
            return View(phieu_Xuat_Kho_Chua);
        }

        // GET: Phieu_Xuat_Kho_Chua/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua = db.Phieu_Xuat_Kho_Chua.Find(id);
            if (phieu_Xuat_Kho_Chua == null)
            {
                return HttpNotFound();
            }
            return View(phieu_Xuat_Kho_Chua);
        }

        // POST: Phieu_Xuat_Kho_Chua/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phieu_Xuat_Kho_Chua phieu_Xuat_Kho_Chua = db.Phieu_Xuat_Kho_Chua.Find(id);
            db.Phieu_Xuat_Kho_Chua.Remove(phieu_Xuat_Kho_Chua);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult InsertPhieuXuat(List<Phieu_Nhap_Json> json)
        {
            ManagerLogin managerLogin = new ManagerLogin();
            decimal tongTien = 0;
            var phieuXuat = new Phieu_Xuat();
            phieuXuat.Ngay_Xuat = DateTime.Now;
            phieuXuat.Tong_Tien = 0;
            phieuXuat.UserId =ManagerLogin.GetId(User.Identity.Name);
            db.Phieu_Xuat.Add(phieuXuat);
            db.SaveChanges();
            var manager = new ManagerPhieuXuat();
            foreach (var item in json)
            {
                tongTien += item.So_Luong * item.Don_gia;
                manager.ThemPhieuXuatHH(phieuXuat.Phieu_Xuat_Id, item);
            }

            //cap nhat tong tien
            phieuXuat.Tong_Tien = tongTien;
            db.Entry(phieuXuat).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {ok = true,newurl = Url.Action("InPhieuXuat","Phieu_Xuat_Kho_Chua",new { id =phieuXuat.Phieu_Xuat_Id }) });
        }

        public ActionResult InPhieuXuat(int? id)
        {
            if(id == null||id>db.Phieu_Xuat.Count())
            {
                return RedirectToAction("Index", "Hang_Hoa");
            }
            var list = db.Phieu_Xuat_Kho_Chua.Where(x => x.Phieu_Xuat_Id == id).ToList();
            ViewBag.px = db.Phieu_Xuat.Find(id);
            return View(list);
        }

        public ActionResult DSPX(int? page)
        {

            var pn = db.Phieu_Xuat.ToList();
            int pageNumber = (page ?? 1);
            return View(pn.ToPagedList(pageNumber, 5));
        }


    }
}
          
   