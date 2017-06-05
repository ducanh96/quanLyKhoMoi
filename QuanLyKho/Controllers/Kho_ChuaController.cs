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
    public class Kho_ChuaController : Controller
    {
        private QuanLyKhoEntities db = new QuanLyKhoEntities();

        // GET: Kho_Chua
        public ActionResult Index()
        {
            return View(db.Kho_Chua.ToList());
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
