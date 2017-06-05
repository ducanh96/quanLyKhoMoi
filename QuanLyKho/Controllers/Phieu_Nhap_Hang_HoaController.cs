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
using QuanLyKho.Models.ModelEntities;
using System.Data.Entity.Core.Objects;
using QuanLyKho.Models;
using PagedList;
using QuanLyKho.Security;
using QuanLyKho.Models.ModelManager;

namespace QuanLyKho.Controllers
{
    [Authorize]
    public class Phieu_Nhap_Hang_HoaController : Controller
    {

        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Phieu_Nhap_Hang_Hoa

        [AuthorizeRole(new string[]{"Admin","Nhap kho"})]
        public ActionResult Index2()
        {
            var list = db.Hang_Hoa.ToList();
            return View(list);
        }

        // GET: Phieu_Nhap_Hang_Hoa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Nhap_Hang_Hoa phieu_Nhap_Hang_Hoa = db.Phieu_Nhap_Hang_Hoa.Find(id);
            if (phieu_Nhap_Hang_Hoa == null)
            {
                return HttpNotFound();
            }
            return View(phieu_Nhap_Hang_Hoa);
        }

    
        // GET: Phieu_Nhap_Hang_Hoa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Nhap_Hang_Hoa phieu_Nhap_Hang_Hoa = db.Phieu_Nhap_Hang_Hoa.Find(id);
            if (phieu_Nhap_Hang_Hoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phieu_Nhap_Id = new SelectList(db.Phieu_Nhap, "Phieu_Nhap_Id", "Phieu_Nhap_Id", phieu_Nhap_Hang_Hoa.Phieu_Nhap_Id);
            ViewBag.Hang_Hoa_id = new SelectList(db.Hang_Hoa, "Hang_Hoa_Id", "Ten_Hang_Hoa", phieu_Nhap_Hang_Hoa.Hang_Hoa_id);
            return View(phieu_Nhap_Hang_Hoa);
        }

        // POST: Phieu_Nhap_Hang_Hoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hang_Hoa_id,Phieu_Nhap_Id,Don_gia,So_Luong")] Phieu_Nhap_Hang_Hoa phieu_Nhap_Hang_Hoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieu_Nhap_Hang_Hoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phieu_Nhap_Id = new SelectList(db.Phieu_Nhap, "Phieu_Nhap_Id", "Phieu_Nhap_Id", phieu_Nhap_Hang_Hoa.Phieu_Nhap_Id);
            ViewBag.Hang_Hoa_id = new SelectList(db.Hang_Hoa, "Hang_Hoa_Id", "Ten_Hang_Hoa", phieu_Nhap_Hang_Hoa.Hang_Hoa_id);
            return View(phieu_Nhap_Hang_Hoa);
        }

        // GET: Phieu_Nhap_Hang_Hoa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieu_Nhap_Hang_Hoa phieu_Nhap_Hang_Hoa = db.Phieu_Nhap_Hang_Hoa.Find(id);
            if (phieu_Nhap_Hang_Hoa == null)
            {
                return HttpNotFound();
            }
            return View(phieu_Nhap_Hang_Hoa);
        }

        // POST: Phieu_Nhap_Hang_Hoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phieu_Nhap_Hang_Hoa phieu_Nhap_Hang_Hoa = db.Phieu_Nhap_Hang_Hoa.Find(id);
            db.Phieu_Nhap_Hang_Hoa.Remove(phieu_Nhap_Hang_Hoa);
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
        public ActionResult InsertPhieuNhap(List<Phieu_Nhap_Json> json)
        {
            ManagerLogin managerLogin = new ManagerLogin();
            //them moi phieu nhap
            decimal tong_tien = 0;
            Phieu_Nhap pn = new Phieu_Nhap();
           
            pn.Ngay_Nhap = DateTime.Now;
            pn.Tong_Tien = 0;
            pn.UserId = ManagerLogin.GetId(User.Identity.Name);
            db.Phieu_Nhap.Add(pn);
            db.SaveChanges();
            //int maPhieu = db.sp_Ma_Phieu_Gan_Nhat().FirstOrDefault().Phieu_Nhap_Id;            
            //int maPhieu = Convert.ToInt32(db.sp_Ma_Phieu_Gan_Nhat().FirstOrDefault().Value);
           
            ManagerPhieuNhap manager = new ManagerPhieuNhap();
            foreach (var item in json)
            {
                manager.ThemPhieuNhapHH(pn.Phieu_Nhap_Id, item);
                tong_tien += item.Don_gia * item.So_Luong;
            }

            //db.sp_UpdateTongTien(pn.Phieu_Nhap_Id, tong_tien);
            pn.Tong_Tien = tong_tien;
            db.Entry(pn).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Ok = true, newurl = Url.Action("InPhieuNhap", new {id =pn.Phieu_Nhap_Id }) });
        }

        public ActionResult InPhieuNhap(int? id)
        {
            if (id == null || id > db.Phieu_Nhap.Count())
            {
                return RedirectToAction("Index", "Hang_Hoa");
            }
            var list = db.Phieu_Nhap_Hang_Hoa.Where(x => x.Phieu_Nhap_Id == id).ToList();
            ViewBag.pn = db.Phieu_Nhap.Find(id);
            return View(list);
        }

        public ActionResult DSPN(int?page)
        {

            var pn = db.Phieu_Nhap.Include("Table_User").ToList();
            int pageNumber = (page ?? 1);
            return View(pn.ToPagedList(pageNumber,5));
        }

    }
}
