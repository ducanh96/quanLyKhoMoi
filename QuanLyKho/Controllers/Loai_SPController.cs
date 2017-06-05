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
    public class Loai_SPController : Controller
    {
        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Loai_SP
        public ActionResult Index()
        {
            return View(db.Loai_SP.ToList());
        }

        // GET: Loai_SP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_SP loai_SP = db.Loai_SP.Find(id);
            if (loai_SP == null)
            {
                return HttpNotFound();
            }
            return View(loai_SP);
        }

        // GET: Loai_SP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loai_SP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Loai_Sp_Id,Ten_Loai")] Loai_SP loai_SP)
        {
            if (ModelState.IsValid)
            {
                var lsp = db.Loai_SP.Where(x => x.Ten_Loai.ToLower().Trim() == loai_SP.Ten_Loai.Trim().ToLower()).FirstOrDefault();
                if(lsp == null)
                {
                    db.Loai_SP.Add(loai_SP);
                    db.SaveChanges();
                    //return Redirect(Request.UrlReferrer.ToString());
                    Response.Write("<script>alert('Thêm mới thành công')</script>");
                }
                else
                {
                    ModelState.AddModelError("errTonTai", "Đã tồn tại loại sản phẩm này");
                }


               
            }

            return View(loai_SP);
        }

        // GET: Loai_SP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_SP loai_SP = db.Loai_SP.Find(id);
            if (loai_SP == null)
            {
                return HttpNotFound();
            }
            return View(loai_SP);
        }

        // POST: Loai_SP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Loai_Sp_Id,Ten_Loai")] Loai_SP loai_SP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loai_SP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai_SP);
        }

        // GET: Loai_SP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_SP loai_SP = db.Loai_SP.Find(id);
            if (loai_SP == null)
            {
                return HttpNotFound();
            }
            return View(loai_SP);
        }

        // POST: Loai_SP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loai_SP loai_SP = db.Loai_SP.Find(id);
            db.Loai_SP.Remove(loai_SP);
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
