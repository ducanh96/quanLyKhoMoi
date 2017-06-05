using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models.ModelDB;

namespace QuanLyKho.Controllers
{
    [Authorize]
    public class Nha_Cung_CapController : Controller
    {
        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Nha_Cung_Cap
        public ActionResult Index()
        {
            return View(db.Nha_Cung_Cap.ToList());
        }

        // GET: Nha_Cung_Cap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_Cung_Cap nha_Cung_Cap = db.Nha_Cung_Cap.Find(id);
            if (nha_Cung_Cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_Cung_Cap);
        }

        // GET: Nha_Cung_Cap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nha_Cung_Cap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nha_Cung_Cap_Id,Ten_Nha_Cung_Cap")] Nha_Cung_Cap nha_Cung_Cap)
        {
            if (ModelState.IsValid)
            {
                var ncc = db.Nha_Cung_Cap.Where(x => x.Ten_Nha_Cung_Cap.ToLower().Trim() == nha_Cung_Cap.Ten_Nha_Cung_Cap.Trim().ToLower()).FirstOrDefault();
                if(ncc==null)
                {
                    db.Nha_Cung_Cap.Add(nha_Cung_Cap);
                    db.SaveChanges();
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    ModelState.AddModelError("errTonTai", "Nhà cung cấp này đã tồn tại");
                }
              
            }

            return View(nha_Cung_Cap);
        }

        // GET: Nha_Cung_Cap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_Cung_Cap nha_Cung_Cap = db.Nha_Cung_Cap.Find(id);
            if (nha_Cung_Cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_Cung_Cap);
        }

        // POST: Nha_Cung_Cap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nha_Cung_Cap_Id,Ten_Nha_Cung_Cap")] Nha_Cung_Cap nha_Cung_Cap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nha_Cung_Cap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nha_Cung_Cap);
        }

        // GET: Nha_Cung_Cap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_Cung_Cap nha_Cung_Cap = db.Nha_Cung_Cap.Find(id);
            if (nha_Cung_Cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_Cung_Cap);
        }

        // POST: Nha_Cung_Cap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nha_Cung_Cap nha_Cung_Cap = db.Nha_Cung_Cap.Find(id);
            db.Nha_Cung_Cap.Remove(nha_Cung_Cap);
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
    }
}
