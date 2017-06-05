using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models.ModelDB;
using System.Threading;

namespace QuanLyKho.Controllers
{
    [Authorize]
    public class Hang_HoaController : Controller
    {
        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Hang_Hoa
        public ActionResult Index()
        {

            var list = db.Hang_Hoa.ToList();
            return View(list);
        }

        // GET: Hang_Hoa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang_Hoa hang_Hoa = db.Hang_Hoa.Find(id);
            if (hang_Hoa == null)
            {
                return HttpNotFound();
            }
            return View(hang_Hoa);
        }

        // GET: Hang_Hoa/Create
        public ActionResult Create()
        {
            ViewBag.Ma_Loai = new SelectList(db.Loai_SP, "Loai_Sp_Id", "Ten_Loai");
            ViewBag.Nha_Cung_Cap_Id = new SelectList(db.Nha_Cung_Cap, "Nha_Cung_Cap_Id", "Ten_Nha_Cung_Cap");
            return View();
        }

        // POST: Hang_Hoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hang_Hoa_Id,Ten_Hang_Hoa,Ma_Loai,IsActive,Nha_Cung_Cap_Id")] Hang_Hoa hang_Hoa)
        {
            if (ModelState.IsValid)
            {
                db.Hang_Hoa.Add(hang_Hoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_Loai = new SelectList(db.Loai_SP, "Loai_Sp_Id", "Ten_Loai", hang_Hoa.Ma_Loai);
            ViewBag.Nha_Cung_Cap_Id = new SelectList(db.Nha_Cung_Cap, "Nha_Cung_Cap_Id", "Ten_Nha_Cung_Cap", hang_Hoa.Nha_Cung_Cap_Id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Hang_Hoa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang_Hoa hang_Hoa = db.Hang_Hoa.Find(id);
            if (hang_Hoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_Loai = new SelectList(db.Loai_SP, "Loai_Sp_Id", "Ten_Loai", hang_Hoa.Ma_Loai);
            ViewBag.Nha_Cung_Cap_Id = new SelectList(db.Nha_Cung_Cap, "Nha_Cung_Cap_Id", "Ten_Nha_Cung_Cap", hang_Hoa.Nha_Cung_Cap_Id);
            return View(hang_Hoa);
        }

        // POST: Hang_Hoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hang_Hoa_Id,Ten_Hang_Hoa,Ma_Loai,IsActive,Nha_Cung_Cap_Id")] Hang_Hoa hang_Hoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hang_Hoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_Loai = new SelectList(db.Loai_SP, "Loai_Sp_Id", "Ten_Loai", hang_Hoa.Ma_Loai);
            ViewBag.Nha_Cung_Cap_Id = new SelectList(db.Nha_Cung_Cap, "Nha_Cung_Cap_Id", "Ten_Nha_Cung_Cap", hang_Hoa.Nha_Cung_Cap_Id);
            return View(hang_Hoa);
        }

        // GET: Hang_Hoa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang_Hoa hang_Hoa = db.Hang_Hoa.Find(id);
            if (hang_Hoa == null)
            {
                return HttpNotFound();
            }
            return View(hang_Hoa);
        }

        // POST: Hang_Hoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hang_Hoa hang_Hoa = db.Hang_Hoa.Find(id);
            db.Hang_Hoa.Remove(hang_Hoa);
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
        [ValidateAntiForgeryToken]
        public ActionResult CreateLoai_SP(Loai_SP loai_SP)
        {
            Thread.Sleep(500);
            string thongBao = "";
          
            if (loai_SP.Ten_Loai !=null)
            {
                var lsp = db.Loai_SP.Where(x => x.Ten_Loai.ToLower().Trim() == loai_SP.Ten_Loai.Trim().ToLower()).FirstOrDefault();
                if (lsp == null)
                {
                    db.Loai_SP.Add(loai_SP);
                    db.SaveChanges();

                    //Response.Write("<script>alert('Thêm mới thành công')</script>");
                    thongBao = "Thêm mới thành công";
                 
                   
                }
                else
                {
                    //ModelState.AddModelError("errTonTai", "Đã tồn tại loại sản phẩm này");
                    thongBao = "Đã tồn tại loại sản phầm này";
                   
                 
                }



            }
            else
            {
               thongBao =  "Hãy nhập tên loại";
               
            }

            
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(thongBao);
            return PartialView("_PartialView",builder.ToString());
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNCC(Nha_Cung_Cap nha_Cung_Cap)
        {
            Thread.Sleep(500);
            string thongBao = "";
            if (nha_Cung_Cap.Ten_Nha_Cung_Cap!=null)
            {
                
                var ncc = db.Nha_Cung_Cap.Where(x => x.Ten_Nha_Cung_Cap.ToLower().Trim() == nha_Cung_Cap.Ten_Nha_Cung_Cap.Trim().ToLower()).FirstOrDefault();
                if (ncc == null)
                {
                    db.Nha_Cung_Cap.Add(nha_Cung_Cap);
                    db.SaveChanges();
                    thongBao = "Thêm mới thành công";
                }
                else
                {
                    thongBao = "Đã tồn tại nhà cung cấp này";
                }

            }
            else
            {
                thongBao =  "Hãy nhập tên nhà cung cấp";
            }
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(thongBao);
            return PartialView("_PartialNCC",builder.ToString());
        }

    }



}

