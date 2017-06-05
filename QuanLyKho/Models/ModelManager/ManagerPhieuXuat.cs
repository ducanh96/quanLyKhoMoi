using QuanLyKho.Models.ModelDB;
using QuanLyKho.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models.ModelManager
{
    public class ManagerPhieuXuat
    {
        public bool ThemPhieuXuatHH(int ma_phieu, Phieu_Nhap_Json pn)
        {
            using (QuanLyKhoEntities db = new QuanLyKhoEntities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var pxhh = new Phieu_Xuat_Kho_Chua();
                        pxhh.Phieu_Xuat_Id = ma_phieu;
                        pxhh.Don_Gia = pn.Don_gia;
                        pxhh.So_Luong = pn.So_Luong;
                        pxhh.Hang_Hoa_Id = pn.Hang_Hoa_id;
                        db.Phieu_Xuat_Kho_Chua.Add(pxhh);
                        var kho = db.Kho_Chua.Find(pxhh.Hang_Hoa_Id);
                        if (kho.So_Luong >= pxhh.So_Luong)
                        {
                            kho.So_Luong -= pxhh.So_Luong;
                            db.Entry(kho).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                            throw new Exception();







                        db.SaveChanges();
                        tran.Commit();

                        return true;

                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }

        }
    }
}